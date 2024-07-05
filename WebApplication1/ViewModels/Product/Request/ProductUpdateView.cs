namespace WebApplication1.ViewModels.Product.Request
{
  public class ProductUpdateView
  {
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public int Quantity { get; set; }
  }
}
