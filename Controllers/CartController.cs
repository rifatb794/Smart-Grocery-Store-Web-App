using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Smart_Grocery_Store_Web_App.Models;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Grocery_Store_Web_App.Controllers
{
    public class CartController : Controller
    {
        // =========================
        // CART PAGE
        // =========================
        public IActionResult Index()
        {
            return View(GetCart());
        }

        // =========================
        // ADD TO CART (AJAX)
        // =========================
        [HttpPost]
        public JsonResult AddAjax(int id)
        {
            if (HttpContext.Session.GetString("USER_NAME") == null)
                return Json(new { login = false });

            var cart = GetCart();
            var product = FakeDb.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return Json(new { success = false });

            var item = cart.FirstOrDefault(x => x.ProductId == id);

            // ✅ STOCK CHECK
            if (item != null && item.Quantity >= product.Stock)
            {
                return Json(new
                {
                    success = false,
                    message = $"Only {product.Stock} items available!"
                });
            }

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            SaveCart(cart);

            return Json(new
            {
                success = true,
                count = cart.Sum(x => x.Quantity)
            });
        }

        // =========================
        // UPDATE QTY
        // =========================
        [HttpPost]
        public JsonResult UpdateQty(int id, int qty)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == id);
            var product = FakeDb.Products.FirstOrDefault(p => p.Id == id);

            if (item != null && product != null)
            {
                if (qty < 1)
                    qty = 1;

                if (qty > product.Stock)
                    qty = product.Stock;

                item.Quantity = qty;
                SaveCart(cart);
            }

            return Json(new
            {
                total = cart.Sum(x => x.Total),
                count = cart.Sum(x => x.Quantity)
            });
        }

        // =========================
        // REMOVE ITEM
        // =========================
        [HttpPost]
        public JsonResult Remove(int id)
        {
            var cart = GetCart();
            cart.RemoveAll(x => x.ProductId == id);
            SaveCart(cart);

            return Json(new
            {
                total = cart.Sum(x => x.Total),
                count = cart.Sum(x => x.Quantity)
            });
        }

        // =========================
        // SESSION CART
        // =========================
        private List<CartItem> GetCart()
        {
            var json = HttpContext.Session.GetString("CART");
            return json == null
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(json)!;
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString(
                "CART",
                JsonConvert.SerializeObject(cart)
            );
        }
    }
}
