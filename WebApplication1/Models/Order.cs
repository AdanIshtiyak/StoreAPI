using WebApplication1.Enum;

namespace WebApplication1.Models
{
  public class Order
  {
    /// <summary>
    /// PK
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 0 - Waiting
    /// 1 - Completed
    /// 2 - Cenceled
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// IsDeleted
    /// </summary>
    public bool IsDeleted { get; set; }

    #region Relationships

    public virtual ICollection<ProductOrderMap> ProductOrderMap { get; set; }

    #endregion
  }
}
