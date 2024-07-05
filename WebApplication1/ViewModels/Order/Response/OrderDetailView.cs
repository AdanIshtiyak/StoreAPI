using WebApplication1.ViewModels.Order.Request;

namespace WebApplication1.ViewModels.Order.Response
{
    public class OrderDetailView
  {
    public int OrderId { get; set; }

    public List<OrderProductDetailView> OrderedProducts { get; set; }
  }
}
