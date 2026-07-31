namespace Smart_Grocery_Store_Web_App.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        // ✅ decimal to match Cart/Order calculation
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string Category { get; set; } = "";

        public bool IsPopular { get; set; } = false;

        public string ImageUrl { get; set; } = "";
    }
}
