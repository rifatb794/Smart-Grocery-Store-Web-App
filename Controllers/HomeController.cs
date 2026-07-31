using Microsoft.AspNetCore.Mvc;
using Smart_Grocery_Store_Web_App.Models;
using System.Linq;

namespace Smart_Grocery_Store_Web_App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var offers = FakeDb.Offers
                .Where(o => o.IsActive)
                .ToList();

            // Popular products ViewBag এ পাঠাচ্ছি
            ViewBag.PopularProducts = FakeDb.Products
                .Where(p => p.IsPopular)
                .ToList();

            return View(offers);
        }
    }
}
