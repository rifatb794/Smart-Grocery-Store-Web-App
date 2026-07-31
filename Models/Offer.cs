using System;

namespace Smart_Grocery_Store_Web_App.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsActive { get; set; }
    }
}
