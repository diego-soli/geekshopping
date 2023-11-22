
namespace GeekShopping.Web.Models
{
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeader { get; set; }
        #nullable enable
        public IEnumerable<CartDetailViewModel>? CartDetails { get; set; }


    }
}
