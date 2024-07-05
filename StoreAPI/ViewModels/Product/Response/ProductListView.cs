namespace StoreAPI.ViewModels.Product.Response
{
  public class ProductListView : IResponseContext
  {
    public List<ProductDetailView> DataList { get; set; }
  }
}
