namespace StoreAPI.ViewModels.Product.Response
{
  public class ProductDetailView
  {
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public int Quantity { get; set; }
  }
}
