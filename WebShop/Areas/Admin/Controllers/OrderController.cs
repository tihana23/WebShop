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
    public class OrderController : Controller
    {
       

        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Order.ToListAsync();
            return View(orders);
        }
     
        public async Task<IActionResult> Details(int id)
        {

            var orders = await _context.Order.FirstOrDefaultAsync(c => c.Id == id);



            if (orders == null)
            {
                return NotFound();

            }
            //var orderProducts = await _context.OrderProduct.Where(p=>p.OrderId ==id).ToListAsync();
            //order.OrderProducts = orderProducts;
            return View(orders);
        }
      
      
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _context.Users.Select(user =>
            new SelectListItem
            {
                Value = user.Id.ToString(),
                Text = user.FirstName + " " + user.LastName
            }
            
            ).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public async Task<IActionResult> Create([Bind("Id,DateCreated,Total,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return View(order);

        }
      
        public async Task<IActionResult> Edit(int id)
        {

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();

            }

            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateCreated,Total,UserId\"")] Order order)
        {


            if (id != order.Id)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!_context.Order.Any(o=>o.Id==id))
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
            return View(order);


        }
      
        public async Task<IActionResult> Delete(int id)
        {

            var order = await _context.Order.FirstOrDefaultAsync(c => c.Id == id);
            if (order == null)
            {
                return NotFound();

            }

            return View(order);
        }

        [HttpPost, ActionName(
            "Delete")]
        [ValidateAntiForgeryToken]
   
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        private bool OrderExist(int id)
        {

            return _context.Order.Any(o => o.Id == id);
        }

    }
}

