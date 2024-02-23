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
    }

    }
}
