using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Diagnostics;
using WebShop.Data;
using WebShop.Extensions;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public const string SessionKeyName = "cart";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> Product(int? categodyId) 
        {
        
        var productsIds = await _context.ProductCategory.Where(p => p.Id==categodyId).Select(p => p.ProductId).ToListAsync();
            var products = await _context.Product.Where(p=>productsIds.Contains(p.Id)).ToListAsync();
            ViewBag.Categories = await _context.Category.Select(c =>
            new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name

            }).ToListAsync();
        return View(products);
        }


        public IActionResult Order(List<string> error)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            if(cart.Count == 0) {
            return RedirectToAction(nameof(Index));
            }
            decimal sum =0;
            ViewBag.TotalPrice = cart.Sum(item => sum + item.GetTotal());

            ViewBag.Error = error;
            return View(cart);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(Order order) {

            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            if (cart.Count == 0) { 
            
            return RedirectToAction(nameof(Index)); 
            }

            var modelErrors = new List<string>();
            if(ModelState.IsValid)
            {
                List<OrderProduct> orderProducts = new List<OrderProduct>();
                foreach (var cartItem in cart) 
                {
                    OrderProduct orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.Product.Id,
                        Quantity = cartItem.Quantity,
                        Total = cartItem.GetTotal()
                    };
                    orderProducts.Add(orderProduct);
                }

                _context.Order.Add(order);
                _context.SaveChanges();
                    HttpContext.Session.SetObjectAsJson(SessionKeyName, "");
                    return RedirectToAction(nameof(Index), new { message = "Ty for your order" });

            }
            else
            {
                foreach(var modelState in ModelState.Values) {

                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
            }
            return RedirectToAction(nameof(Order),new { errors = modelErrors});


        }
           
            

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
