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
       
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Quantity,Price")] Product products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return View(products);

        }
     
        public async Task<IActionResult> Edit(int id)
        {

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();

            }

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Quantity,Price")] Product products)
        {


            if (id != products.Id)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!_context.Product.Any(o => o.Id == id))
                    {

                        return NotFound();
                    }
                    else
                    {
                        throw ex;
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            return View(products);


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
