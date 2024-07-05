using WebApplication1.ViewModels.Product.Request;
using WebApplication1.ViewModels.Product.Response;

namespace WebApplication1.Interfaces
{
  public interface IProductService
  {
    /// <summary>
    /// CreateProduct
    /// </summary>
    /// <param name="request"></param>
    void CreateProduct(ProductCreateView request);

    /// <summary>
    /// UpdateProduct
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    void UpdateProduct(ProductUpdateView request);

    /// <summary>
    /// DeleteProduct
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    void DeleteProduct(int productId);

    /// <summary>
    /// GetProduct
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    ProductDetailView GetProduct(int productId);

    /// <summary>
    /// GetProducts
    /// </summary>
    /// <returns></returns>
    Task<ProductListView> GetProducts();

    #region Internal methods

    /// <summary>
    /// GetInternalProductsQuantity
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<List<ProductQuantityView>> GetInternalProductsQuantity(List<int> productIds);

    #endregion
  }
}
