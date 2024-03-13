using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
       
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.ToListAsync();
            return View(products);
        }
        
        public async Task<IActionResult> Details(int id)
        {

            var products = await _context.Product.FirstOrDefaultAsync(c => c.Id == id);



            if (products == null)
            {
                return NotFound();

            }
            //var orderProducts = await _context.OrderProduct.Where(p=>p.OrderId ==id).ToListAsync();
            //order.products = products;
            return View(products);
        }
       
        public async Task<IActionResult> Create()
        {

            ViewBag.Categories = await _context.Category.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToListAsync();

           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product)
        {
            if (ModelState.IsValid)
            {
                List<ProductCategory> productCategories = new List<ProductCategory>();

                foreach (var categoryId in product.Categories)
                {
                    var productCategory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        ProductId = product.Id
                    };
                    productCategories.Add(productCategory);
                }
                product.ProductCategories = productCategories;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Product
                .Include(p=>p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await _context.Category.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToListAsync();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Quantity,Price")] Product product, int CategoryId)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    // Update or create the ProductCategory link
                    var productCategory = await _context.ProductCategory.FirstOrDefaultAsync(pc => pc.ProductId == product.Id);
                    if (productCategory != null)
                    {
                        productCategory.CategoryId = CategoryId;
                    }
                    else
                    {
                        _context.ProductCategory.Add(new ProductCategory { ProductId = product.Id, CategoryId = CategoryId });
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Product.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SelectedCategoryId = CategoryId;
            ViewBag.Categories = _context.Category.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var products = await _context.Product.FirstOrDefaultAsync(c => c.Id == id);
            if (products == null)
            {
                return NotFound();

            }

            return View(products);
        }

        [HttpPost, ActionName(
            "Delete")]
        [ValidateAntiForgeryToken]
 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var products = await _context.Product.FindAsync(id);
            _context.Product.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
}
