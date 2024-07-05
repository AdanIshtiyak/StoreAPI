using WebApplication1.Enum;
using WebApplication1.ViewModels.Order.Request;
using WebApplication1.ViewModels.Order.Response;

namespace WebApplication1.Interfaces
{
  public interface IOrderService
  {
    /// <summary>
    /// CreateOrder
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task CreateOrder(OrderCreateView request);

    /// <summary>
    /// OrderUpdateView
    /// </summary>
    /// <param name="request"></param>
    Task UpdateOrder(OrderUpdateView request);

    /// <summary>
    /// GetOrder
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    OrderDetailView? GetOrder(int orderId);

    /// <summary>
    /// GetOrders
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<OrderListView> GetOrders(OrderStatus? status);
  }
}
