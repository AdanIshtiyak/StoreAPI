using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels.Product.Request;
using WebApplication1.ViewModels.Product.Response;

namespace WebApplication1.Services
{
  public class ProductService : IProductService
  {
    private readonly IRepo<Product> _productRepo;

    public ProductService(IRepo<Product> productRepo)
    {
      _productRepo = productRepo;
    }

    #region Public methods

    /// <summary>
    /// CreateProduct
    /// </summary>
    /// <param name="request"></param>
    public void CreateProduct(ProductCreateView request)
    {
      var isDuplicate = _productRepo.GetAll(c => !c.IsDeleted && c.Name.ToLower().Trim() == request.Name.ToLower().Trim()).Any();

      if (isDuplicate)
        return;

      var newProduct = new Product()
      {
        Name = request.Name,
        Description = request.Description,
        Quantity = request.Quantity,
      };

      _productRepo.Create(newProduct);
      _productRepo.SaveChanges();
    }

    /// <summary>
    /// UpdateProduct
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public void UpdateProduct(ProductUpdateView request)
    {
      var isDuplicate = _productRepo.GetAll(c => !c.IsDeleted && c.Id == request.ProductId && c.Name.ToLower().Trim() == request.Name.ToLower().Trim()).Any();

      if (isDuplicate) return;

      var product = _productRepo.FirstOrDefault(c => !c.IsDeleted && c.Id == request.ProductId);

      if (product == null) return;

      product.Name = request.Name;
      product.Description = request.Description;
      product.Quantity = request.Quantity;

      _productRepo.SaveChanges();
    }

    /// <summary>
    /// DeleteProduct
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public void DeleteProduct(int productId)
    {
      var product = _productRepo.FirstOrDefault(c => !c.IsDeleted && c.Id == productId);

      if (product == null) return;

      product.IsDeleted = true;

      _productRepo.SaveChanges();
    }

    /// <summary>
    /// GetProduct
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public ProductDetailView GetProduct(int productId)
    {
      var product = _productRepo.FirstOrDefault(c => !c.IsDeleted && c.Id == productId);

      if (product == null) return new ProductDetailView();

      var result = new ProductDetailView()
      {
        ProductId = product.Id,
        Name = product.Name,
        Description = product.Description,
        Quantity = product.Quantity
      };

      return result;
    }

    /// <summary>
    /// GetProducts
    /// </summary>
    /// <returns></returns>
    public async Task<ProductListView> GetProducts()
    {
      var products = await _productRepo.GetAll(c => !c.IsDeleted).Select(c => new ProductDetailView()
      {
        ProductId = c.Id,
        Name = c.Name,
        Description = c.Description,
        Quantity = c.Quantity
      }).AsNoTracking().ToListAsync();

      var result = new ProductListView()
      {
        DataList = products
      };

      return result;
    }

    #endregion

    #region Internal methods

    /// <summary>
    /// GetInternalProductsQuantity
    /// </summary>
    /// <param name="productIds"></param>
    /// <returns></returns>
    public async Task<List<ProductQuantityView>> GetInternalProductsQuantity(List<int> productIds)
    {
      var result = await _productRepo.GetAll(c => !c.IsDeleted && productIds.Contains(c.Id)).Select(c => new ProductQuantityView()
      {
        ProductId = c.Id,
        Quantity = c.Quantity,
      }).ToListAsync();

      return result;
    }

    #endregion
  }
}
