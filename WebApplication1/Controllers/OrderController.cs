using Microsoft.AspNetCore.Mvc;
using WebApplication1.Enum;
using WebApplication1.Interfaces;
using WebApplication1.ViewModels.Order.Request;
using WebApplication1.ViewModels.Order.Response;

namespace WebApplication1.Controllers
{
  [Route("api/v1/order")]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService _orderService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="orderService"></param>
    public OrderController(IOrderService orderService)
    {
      _orderService = orderService;
    }

    [HttpPost]
    public async Task CreateOrder([FromBody] OrderCreateView request)
    {
      await _orderService.CreateOrder(request);
    }

    [HttpPatch]
    public async Task UpdateOrder([FromBody] OrderUpdateView request)
    {
      await _orderService.UpdateOrder(request);
    }

    [HttpGet]
    public OrderDetailView? GetOrder(int orderId)
    {
      var result = _orderService.GetOrder(orderId);

      return result;
    }

    [HttpGet("list")]
    public async Task<OrderListView> GetOrders(OrderStatus? status)
    {
      var result = await _orderService.GetOrders(status);

      return result;
    }
  }
}
