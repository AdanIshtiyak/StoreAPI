using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
  public class ProductOrderMap
  {
    /// <summary>
    /// PK
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// OrderedQuantity
    /// </summary>
    public int OrderedQuantity { get; set; }

    /// <summary>
    /// ProductId (FK, Product.Id)
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// OrderId (FK, Order.Id)
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    ///IsDeleted
    /// </summary>
    public bool IsDeleted { get; set; }

    #region Relationships

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }

    #endregion
  }
}
