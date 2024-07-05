using StoreAPI.Enum;

namespace StoreAPI.ViewModels.Order.Request
{
    public class OrderCreateView
  {
    public List<OrderedProductsView> OrderedProducts { get; set; }
  }
}
