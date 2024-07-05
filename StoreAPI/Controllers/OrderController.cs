using Microsoft.AspNetCore.Mvc;
using StoreAPI.Enum;
using StoreAPI.Interfaces;
using StoreAPI.ViewModels.Order.Request;
using StoreAPI.ViewModels.Order.Response;

namespace StoreAPI.Controllers
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
