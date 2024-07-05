using Microsoft.EntityFrameworkCore;
using StoreAPI.Enum;
using StoreAPI.Interfaces;
using StoreAPI.Models;
using StoreAPI.ViewModels.Order.Request;
using StoreAPI.ViewModels.Order.Response;

namespace StoreAPI.Services
{
  public class OrderService : IOrderService
  {
    private readonly IRepo<Order> _orderRepo;
    private readonly IRepo<Product> _productRepo;
    private readonly IServiceScopeFactory _scopeFactory;
    private IProductService _productService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="orderRepo"></param>
    /// <param name="productRepo"></param>
    /// <param name="scopeFactory"></param>
    public OrderService(IRepo<Order> orderRepo, IRepo<Product> productRepo,IServiceScopeFactory scopeFactory)
    {
      _orderRepo = orderRepo;
      _productRepo = productRepo;
      _scopeFactory = scopeFactory; 
    }

    /// <summary>
    /// CreateOrder
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task CreateOrder(OrderCreateView request)
    {
      var scope = _scopeFactory.CreateScope();
      _productService = scope.ServiceProvider.GetRequiredService<IProductService>();

      var productsQuantities = await _productService.GetInternalProductsQuantity(request.OrderedProducts.Select(c => c.ProductId).ToList());

      foreach (var orderedProduct in request.OrderedProducts)
      {
        var productQuantity = productsQuantities.FirstOrDefault(c => c.ProductId == orderedProduct.ProductId)?.Quantity ?? 0;
        if (productQuantity < orderedProduct.OrderedQuantity)
          return;
      }

      #region Change products quantity in the stock

      var productList = await _productRepo.GetAll(c => !c.IsDeleted).ToListAsync();

      foreach (var orderedProduct in request.OrderedProducts)
        productList.FirstOrDefault(c => c.Id == orderedProduct.ProductId).Quantity -= orderedProduct.OrderedQuantity;

      #endregion

      var newOrder = new Order()
      {
        Status = OrderStatus.Waiting,
        ProductOrderMap = request.OrderedProducts.Select(c => new ProductOrderMap()
        {
          OrderedQuantity = c.OrderedQuantity,
          ProductId = c.ProductId,
        }).ToList(),
      };

      _orderRepo.Create(newOrder);
      _orderRepo.SaveChanges();
    }

    /// <summary>
    /// OrderUpdateView
    /// </summary>
    /// <param name="request"></param>
    public async Task UpdateOrder(OrderUpdateView request)
    {
      #region Get order

      var order = _orderRepo.GetAll(c => !c.IsDeleted && c.Id == request.OrderId)
        .Include(c => c.ProductOrderMap)
        .FirstOrDefault();

      if (order == null) return;

      #endregion

      #region Check if quantity enough in the stock

      var scope = _scopeFactory.CreateScope();
      _productService = scope.ServiceProvider.GetRequiredService<IProductService>();

      var productsQuantities = await _productService.GetInternalProductsQuantity(request.OrderedProducts.Select(c => c.ProductId).ToList());

      foreach (var orderedProducts in request.OrderedProducts)
      {
        if (productsQuantities.FirstOrDefault(c => c.ProductId == orderedProducts.ProductId).Quantity < orderedProducts.OrderedQuantity)
          return;
      }

      #endregion

      #region Update order

      order.Status = request.Status;

      #region Delete removed products from order

      foreach (var orderedProduct in order.ProductOrderMap.Where(c => !request.OrderedProducts.Select(c => c.ProductId).Contains(c.Id)))
        orderedProduct.IsDeleted = true;

      #endregion

      #region Update product quantity in the stock

      var productList = await _productRepo.GetAll(c => !c.IsDeleted)
        .ToListAsync();

      foreach (var orderedProducts in request.OrderedProducts)
      {
        var productOrderMap = order.ProductOrderMap.FirstOrDefault(c => c.ProductId == orderedProducts.ProductId);
        var product = productList.FirstOrDefault(c => c.Id == orderedProducts.ProductId);

        product.Quantity += productOrderMap.OrderedQuantity;
        product.Quantity -= orderedProducts.OrderedQuantity;
      }

      #endregion

      #region Update ordered products

      var productOrderMapList = order.ProductOrderMap.Where(c => !c.IsDeleted).ToList();

      foreach (var orderedProduct in request.OrderedProducts)
        productOrderMapList.FirstOrDefault(c => c.Id == orderedProduct.ProductId).OrderedQuantity = orderedProduct.OrderedQuantity;

      #endregion

      #endregion

      _orderRepo.SaveChanges();
    }

    /// <summary>
    /// GetOrder
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public OrderDetailView? GetOrder(int orderId)
    {
      var order = _orderRepo.GetAll(c => !c.IsDeleted && c.Id == orderId)
        .Include(c => c.ProductOrderMap)
        .ThenInclude(c => c.Product)
        .FirstOrDefault();

      if (order == null) return null;

      var result = new OrderDetailView()
      {
        OrderId = orderId,
        OrderedProducts = order.ProductOrderMap.Select(c => new OrderProductDetailView()
        {
          ProductId = c.ProductId,
          ProductName = c.Product.Name,
          OrderedQuantity = c.OrderedQuantity
        }).ToList(),
      };

      return result;
    }

    /// <summary>
    /// GetOrders
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<OrderListView> GetOrders(OrderStatus? status)
    {
      var orderList = await _orderRepo.GetAll(c => !c.IsDeleted)
        .Include(c => c.ProductOrderMap)
        .AsNoTracking()
        .ToListAsync();

      if (status.HasValue)
        orderList = orderList.Where(c => c.Status == status).ToList();

      var result = new OrderListView()
      {
        DataList = orderList.Select(c => new OrderSimpleView()
        {
          OrderId = c.Id,
          OrderedProduictsCount = c.ProductOrderMap.Count,
        }).ToList(),
      };

      return result;
    }
  }
}
