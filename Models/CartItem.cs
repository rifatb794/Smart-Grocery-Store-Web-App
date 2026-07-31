namespace Smart_Grocery_Store_Web_App.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;
        // 👆 nullable error fix

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}
