using WebApplication1.Enum;

namespace WebApplication1.ViewModels.Order.Request
{
    public class OrderUpdateView
  {
    public int OrderId { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderedProductsView> OrderedProducts { get; set; }
  }
}