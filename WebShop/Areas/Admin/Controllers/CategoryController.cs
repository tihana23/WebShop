using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Category.ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id) {

            var category = await _context.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();

            }

            return View(category);
        }

        public IActionResult Create() {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name")] Category category) { 
            if(ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }return View(category);
        
        }

        public async Task<IActionResult> Edit(int id) {

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();

            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id, Name")] Category category)
        {

            
            if (id != category.Id)
            {
                return NotFound();

            }
           
                if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CategoryExist(category.Id))
                    {

                        return NotFound();
                    }
                    else { 
                    throw ex;
                    }
                }return RedirectToAction(nameof(Index));

            }
            return View(category);


        }
        public async Task<IActionResult> Delete(int id)
        {

            var category = await _context.Category.FirstOrDefaultAsync(c => c.Id ==id);
            if (category == null)
            {
                return NotFound();

            }

            return View(category);
        }

        [HttpPost,ActionName(
            "Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        private bool CategoryExist(int id)
        {

            return _context.Category.Any(Category => Category.Id == id);
        }
          
        }
    }
    


