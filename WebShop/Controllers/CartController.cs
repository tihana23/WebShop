﻿using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;

        public const string SessionKeyName = "cart";


        public CartController(ApplicationDbContext context) { 
        _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            decimal sum = 0;
            ViewBag.TotalPrice = cart.Sum(item => sum + item.GetTotal());

            return View(cart);
        }

        [HttpPost]

        public IActionResult AddToCart(int productId) {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            // Postoji li proizvod u košarici, dodaj kolicinu
            if (cart.Select(c => c.Product).Any(p => p.Id == productId))
            {
                cart.First(c => c.Product.Id == productId).Quantity++;
            }

            else // Ne postoji proizvod u košarici, dodaj proizvod, kolicina = 1
            {
                CartItem cartItem = new CartItem()
                {
                    Product = _context.Product.Find(productId),
                    Quantity = 1
                };

                cart.Add(cartItem);

               
            }
            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart); // dodaj ga u sesiju
            return RedirectToAction(nameof(Index));


        }
        
        public IActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(p=>p.Product.Id== productId);
            if(cartItem != null)
            {

                cart.Remove(cartItem);
                HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);



            }
            return RedirectToAction(nameof(Index));
        }


    }
}
