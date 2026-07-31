using Microsoft.AspNetCore.Mvc;
using Smart_Grocery_Store_Web_App.Models;
using System.Linq;

public class AuthController : Controller
{
    // ---------- LOGIN ----------
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password, string role)
    {
        var user = FakeDb.Users.FirstOrDefault(u =>
            u.Username == username &&
            u.Password == password &&
            u.Role == role
        );

        if (user == null)
        {
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        HttpContext.Session.SetString("USER_NAME", user.Username);
        HttpContext.Session.SetString("USER_ROLE", user.Role);

        // ✅ FIXED
        if (role == "Admin")
        {
            return RedirectToAction("Index", "Admin");
        }

        return RedirectToAction("Index", "Home");
    }

    // ---------- REGISTER ----------
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string password)
    {
        if (FakeDb.Users.Any(u => u.Username == username))
        {
            ViewBag.Error = "Username already exists";
            return View();
        }

        FakeDb.Users.Add(new User
        {
            Id = FakeDb.Users.Count + 1,
            Username = username,
            Password = password,
            Role = "User"
        });

        ViewBag.Success = "Registration successful. Please login.";
        return View();
    }

    // ---------- LOGOUT ----------
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
