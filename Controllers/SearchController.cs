using Microsoft.AspNetCore.Mvc;
using Smart_Grocery_Store_Web_App.Models;
using System.Linq;

namespace Smart_Grocery_Store_Web_App.Controllers
{
    public class SearchController : Controller
    {
        public JsonResult Suggest(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return Json(new object[] { });

            var result = FakeDb.Products
                .Where(p => p.Name.ToLower().Contains(q.ToLower()))
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    price = p.Price
                })
                .Take(8)
                .ToList();

            return Json(result);
        }
    }
}
