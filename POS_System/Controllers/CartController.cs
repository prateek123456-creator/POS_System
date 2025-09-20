using Microsoft.AspNetCore.Mvc;
using POS_System.Models;
using System.Collections.Generic;
using System.Linq;

namespace POS_System.Controllers
{
    public class CartController : Controller
    {
        private static List<CartItem> _cart = new();
        private static List<CartItem> _suspendedCart = new(); 
        private static decimal _discount = 0;
        private static int _nextProductId = 1;
        private const decimal TaxRate = 0.04m;

        public IActionResult Index()
        {
            decimal subtotal = _cart.Sum(c => c.Subtotal);
            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax;

            ViewBag.CartItems = _cart;
            ViewBag.TotalItems = _cart.Sum(c => c.Quantity);
            ViewBag.Subtotal = subtotal;
            ViewBag.Tax = tax;
            ViewBag.Total = total;
            ViewBag.Discount = _discount;
            ViewBag.Payable = total - _discount;

            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name) || price <= 0 || quantity <= 0)
                return RedirectToAction("Index");

            var existing = _cart.FirstOrDefault(c => c.Product.Name == name && c.Product.Price == price);
            if (existing != null)
            {
                existing.Quantity += quantity; 
            }
            else
            {
                var product = new Product
                {
                    Id = _nextProductId++,
                    Name = name,
                    Price = price
                };

                _cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var item = _cart.FirstOrDefault(c => c.Product.Id == productId);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            _cart.RemoveAll(c => c.Product.Id == productId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ApplyDiscount(decimal discount, string type)
        {
            var subtotal = _cart.Sum(c => c.Subtotal);
            var tax = subtotal * TaxRate;
            var total = subtotal + tax;

            if (type == "percent")
            {
               
                _discount = (discount / 100m) * total;
            }
            else
            {
            
                _discount = discount;
            }

          
            if (_discount > total) _discount = total;

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Cancel()
        {
            _cart.Clear();
            _discount = 0;
            _nextProductId = 1;
            return RedirectToAction("Index");
        }

        private static decimal _suspendedDiscount = 0;
        [HttpPost]
        public IActionResult Suspend()
        {
            _suspendedCart = new List<CartItem>(_cart.Select(c => new CartItem
            {
                Product = c.Product,
                Quantity = c.Quantity
            }));

            _suspendedDiscount = _discount; 

            _cart.Clear();
            _discount = 0;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Restore()
        {
            if (_suspendedCart.Any())
            {
                _cart = new List<CartItem>(_suspendedCart.Select(c => new CartItem
                {
                    Product = c.Product,
                    Quantity = c.Quantity
                }));
                _discount = _suspendedDiscount; 
                _suspendedCart.Clear();
                _suspendedDiscount = 0;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Bill()
        {
            decimal subtotal = _cart.Sum(c => c.Subtotal);
            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax;
            decimal payable = total - _discount;

            TempData["Message"] = $"🧾 Bill Preview → Subtotal: ₹{subtotal}, Tax: ₹{tax}, Discount: ₹{_discount}, Final: ₹{payable}";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Payment()
        {
            decimal subtotal = _cart.Sum(c => c.Subtotal);
            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax;
            decimal payable = total - _discount;

            TempData["Message"] = $"✅ Payment Successful → Final Bill: ₹{payable}";
            return RedirectToAction("Index");
        }
    }
}
