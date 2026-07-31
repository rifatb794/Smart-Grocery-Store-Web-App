using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Smart_Grocery_Store_Web_App.Models;

namespace Smart_Grocery_Store_Web_App.Helpers
{
    public static class CartHelper
    {
        private const string KEY = "CART";

        public static Cart GetCart(HttpContext context)
        {
            var json = context.Session.GetString(KEY);
            if (string.IsNullOrEmpty(json))
                return new Cart();

            return JsonConvert.DeserializeObject<Cart>(json) ?? new Cart();
        }

        public static void Save(HttpContext context, Cart cart)
        {
            context.Session.SetString(KEY, JsonConvert.SerializeObject(cart));
        }

        public static void Clear(HttpContext context)
        {
            context.Session.Remove(KEY);
        }
    }
}
