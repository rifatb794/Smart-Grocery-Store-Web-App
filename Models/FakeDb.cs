using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smart_Grocery_Store_Web_App.Models
{
    public static class FakeDb
    {
        // ================= USERS =================
        public static List<User> Users = new()
        {
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "1234",
                Email = "admin@gmail.com",
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "user",
                Password = "1234",
                Email = "user@gmail.com",
                Role = "User"
            }
        };

        // ================= PRODUCTS =================
        public static List<Product> Products = new()
        {
            new Product { Id=1,  Name="Miniket Rice 5kg", Price=420, Stock=40, Category="Rice", IsPopular=true, ImageUrl="/images/miniket.jpg" },
            new Product { Id=2,  Name="ACI Pure Salt 1kg", Price=35,  Stock=100, Category="Salt", ImageUrl="/images/salt.jpg" },
            new Product { Id=3,  Name="Fresh Sugar 1kg", Price=90,  Stock=80, Category="Sugar", ImageUrl="/images/sugar.jpg" },
            new Product { Id=4,  Name="Potato 1kg", Price=40,  Stock=100, Category="Vegetable", ImageUrl="/images/potato.jpg" },
            new Product { Id=5,  Name="Onion 1kg", Price=65,  Stock=100, Category="Vegetable", ImageUrl="/images/onion.jpg" },
            new Product { Id=6,  Name="Garlic 500g", Price=90,  Stock=50, Category="Vegetable", ImageUrl="/images/garlic.jpg" },
            new Product { Id=7,  Name="Tomato 1kg", Price=60,  Stock=50, Category="Vegetable", ImageUrl="/images/tomato.jpg" },
            new Product { Id=8,  Name="Broiler Chicken 1kg", Price=210, Stock=30, Category="Meat", ImageUrl="/images/chicken.jpg" },
            new Product { Id=9,  Name="Beef 1kg", Price=680, Stock=2, Category="Meat", ImageUrl="/images/beef.jpg" },
            new Product { Id=10, Name="Rui Fish 1kg", Price=320, Stock=40, Category="Fish", ImageUrl="/images/rui.jpg" },

            new Product { Id=11, Name="Milk 1L", Price=80, Stock=40, Category="Dairy", IsPopular=true, ImageUrl="/images/milk.jpg" },
            new Product { Id=12, Name="Egg (12 pcs)", Price=110, Stock=60, Category="Dairy", ImageUrl="/images/egg.jpg" },
            new Product { Id=13, Name="Bread 400g", Price=40, Stock=30, Category="Bakery", ImageUrl="/images/bread.jpg" },
            new Product { Id=14, Name="Rupchanda Oil 1L", Price=180, Stock=80, Category="Oil", ImageUrl="/images/rupchanda.jpg" },
            new Product { Id=15, Name="Tea 200g", Price=120, Stock=50, Category="Beverage", ImageUrl="/images/tea.jpg" },

            new Product { Id=16, Name="Chips (Potato) 25g", Price=20, Stock=150, Category="Snacks", ImageUrl="/images/chips.jpg" },
            new Product { Id=17, Name="Chanachur 400g", Price=50, Stock=90, Category="Snacks", ImageUrl="/images/chanachur.jpg" },
            new Product { Id=18, Name="Detergent Powder 1kg", Price=110, Stock=50, Category="Cleaning", ImageUrl="/images/detergent.jpg" },
            new Product { Id=19, Name="Bath Soap 125g", Price=35, Stock=80, Category="Personal Care", ImageUrl="/images/soap.jpg" },
            new Product { Id=20, Name="Toothpaste 200g", Price=90, Stock=30, Category="Personal Care", ImageUrl="/images/toothpaste.jpg" }
        };

        // ================= OFFERS =================
        public static List<Offer> Offers = new()
        {
            new Offer
            {
                Id = 1,
                Title = "10% Off on Rice",
                Description = "Buy Miniket rice and get discount!",
                ValidFrom = DateTime.Now.AddDays(-1),
                ValidTo = DateTime.Now.AddDays(5),
                IsActive = true
            }
        };

        // ================= ORDERS =================
        public static List<Order> Orders = new()
        {
            new Order
            {
                Id = 1001,
                Status = "Processing",
                OrderDate = DateTime.Now,
                CustomerName = "Demo User",
                Items = new List<CartItem>()
            }
        };

        // ================= AUTO ORDER STATUS =================
        static FakeDb()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    foreach (var order in Orders)
                    {
                        if (order.Status == "Processing")
                            order.Status = "Packed";
                        else if (order.Status == "Packed")
                            order.Status = "Shipped";
                        else if (order.Status == "Shipped")
                            order.Status = "Delivered";
                    }

                    await Task.Delay(5000);
                }
            });
        }
    }
}
