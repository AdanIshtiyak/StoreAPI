using StoreAPI.ViewModels.Order.Request;

namespace StoreAPI.ViewModels.Order.Response
{
    public class OrderDetailView
  {
    public int OrderId { get; set; }

    public List<OrderProductDetailView> OrderedProducts { get; set; }
  }
}
