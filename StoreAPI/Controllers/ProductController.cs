using Microsoft.AspNetCore.Mvc;
using StoreAPI.Interfaces;
using StoreAPI.ViewModels.Product.Request;
using StoreAPI.ViewModels.Product.Response;

namespace StoreAPI.Controllers
{
  [Route("api/v1/product")]
  public class ProductController : ControllerBase
  {
    private readonly IProductService _productService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="productService"></param>
    public ProductController(IProductService productService)
    {
      _productService = productService;
    }

    [HttpPost]
    public void CreateProduct([FromBody] ProductCreateView request)
    {
      _productService.CreateProduct(request);
    }

    [HttpPatch]
    public void UpdateProduct([FromBody] ProductUpdateView request)
    {
      _productService.UpdateProduct(request);
    }

    [HttpDelete]
    public void DeleteProduct(int productId)
    {
      _productService.DeleteProduct(productId);
    }

    [HttpGet]
    public ProductDetailView GetProduct(int productId)
    {
      var result =  _productService.GetProduct(productId);

      return result;
    }

    [HttpGet("list")]
    public async Task<ProductListView> GetProducts()
    {
      var result = await _productService.GetProducts();

      return result;
    }

  }
}
