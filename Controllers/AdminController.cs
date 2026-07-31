using Microsoft.AspNetCore.Mvc;
using Smart_Grocery_Store_Web_App.Models;
using System.Linq;

public class AdminController : Controller
{
    // ================= DASHBOARD =================
    // Shows Orders + Products
    public IActionResult Index()
    {
        var model = Tuple.Create(
            FakeDb.Orders,     // Item1 → Orders
            FakeDb.Products    // Item2 → Products
        );

        return View(model);
    }

    // ================= ORDER STATUS =================
    public IActionResult UpdateStatus(int id, string status)
    {
        var order = FakeDb.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.Status = status;
        }
        return RedirectToAction("Index");
    }

    // ================= PRODUCT CRUD =================

    // ADD (GET)
    public IActionResult Create()
    {
        return View();
    }

    // ADD (POST) + IMAGE UPLOAD
    [HttpPost]
    public IActionResult Create(Product product, IFormFile Image)
    {
        if (Image != null && Image.Length > 0)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images",
                fileName
            );

            using (var stream = new FileStream(path, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            // ✅ correct property
            product.ImageUrl = "/images/" + fileName;
        }

        product.Id = FakeDb.Products.Count + 1;
        FakeDb.Products.Add(product);

        return RedirectToAction("Index");
    }

    // EDIT (GET)
    public IActionResult Edit(int id)
    {
        var product = FakeDb.Products.FirstOrDefault(p => p.Id == id);
        return View(product);
    }

    // EDIT (POST)
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        var p = FakeDb.Products.FirstOrDefault(x => x.Id == product.Id);
        if (p != null)
        {
            p.Name = product.Name;
            p.Price = product.Price;
            p.Stock = product.Stock;
            p.Category = product.Category;
            p.IsPopular = product.IsPopular;
        }
        return RedirectToAction("Index");
    }

    // DELETE
    public IActionResult Delete(int id)
    {
        var product = FakeDb.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            FakeDb.Products.Remove(product);
        }
        return RedirectToAction("Index");
    }
}
