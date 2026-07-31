using Microsoft.AspNetCore.Mvc;
using Smart_Grocery_Store_Web_App.Models;
using System.Linq;

namespace Smart_Grocery_Store_Web_App.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = FakeDb.Products.ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = FakeDb.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
