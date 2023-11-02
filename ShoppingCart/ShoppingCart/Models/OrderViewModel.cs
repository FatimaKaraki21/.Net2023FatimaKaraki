using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Models
{
    public class OrderViewModel
    {

        public Order Order { get; set; }
        public CartViewModel CartViewModel { get; set; }
    }
}
