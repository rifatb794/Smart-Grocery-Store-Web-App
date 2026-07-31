using System.Collections.Generic;
using System.Linq;

namespace Smart_Grocery_Store_Web_App.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new();

        public decimal SubTotal => Items.Sum(x => x.Total);

        public decimal Vat => SubTotal * 0.05m;

        public decimal Discount => SubTotal > 500 ? 50 : 0;

        public decimal GrandTotal => SubTotal + Vat - Discount;
    }
}
