using System.Collections.Generic;

namespace Smart_Grocery_Store_Web_App.Models
{
    public class HomeViewModel
    {
        public List<Offer> ActiveOffers { get; set; } = new();
        public List<Product> PopularProducts { get; set; } = new();
    }
}
