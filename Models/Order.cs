using System;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Grocery_Store_Web_App.Models
{
    public class Order
    {
        public int Id { get; set; }

        public List<CartItem> Items { get; set; } = new();

        public string Status { get; set; } = "Processing";

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string CustomerName { get; set; } = "Guest";

        // ✅ Dynamic calculation (server-side safe)
        public decimal SubTotal => Items.Sum(x => x.Total);

        public decimal Vat => SubTotal * 0.05m;

        public decimal Discount => SubTotal > 500 ? 50 : 0;

        public decimal GrandTotal => SubTotal + Vat - Discount;
    }
}
