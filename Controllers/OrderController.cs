using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuestPDF.Fluent;
using Smart_Grocery_Store_Web_App.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Smart_Grocery_Store_Web_App.Controllers
{
    public class OrderController : Controller
    {
        // =========================
        // PLACE ORDER
        // =========================
        [HttpPost]
        public IActionResult Place()
        {
            var json = HttpContext.Session.GetString("CART");

            var cart = string.IsNullOrEmpty(json)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(json);

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            var order = new Order
            {
                Id = FakeDb.Orders.Count + 1001,
                Items = cart,
                OrderDate = DateTime.Now,
                CustomerName = HttpContext.Session.GetString("USER_NAME") ?? "Guest",
                Status = "Processing"
            };

            FakeDb.Orders.Add(order);

            // clear cart
            HttpContext.Session.Remove("CART");

            return RedirectToAction("Track", new { id = order.Id });
        }

        // =========================
        // TRACK ORDER PAGE
        // =========================
        public IActionResult Track(int id)
        {
            var order = FakeDb.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // =========================
        // LIVE ORDER STATUS (AJAX)
        // =========================
        [HttpGet]
        public JsonResult GetStatus(int id)
        {
            var order = FakeDb.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return Json(new { found = false });

            var elapsed = (DateTime.Now - order.OrderDate).TotalSeconds;

            if (elapsed > 15)
                order.Status = "Delivered";
            else if (elapsed > 10)
                order.Status = "Shipped";
            else if (elapsed > 5)
                order.Status = "Packed";
            else
                order.Status = "Processing";

            return Json(new
            {
                found = true,
                status = order.Status
            });
        }

        // =========================
        // INVOICE PDF (QUESTPDF)
        // =========================
        public IActionResult Invoice(int id)
        {
            var order = FakeDb.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            var document = new InvoiceDocument(order);
            var pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", $"Invoice_{order.Id}.pdf");
        }
    }
}
