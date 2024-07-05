using System.Linq.Expressions;

namespace StoreAPI.Interfaces
{
  public interface IRepo<T> where T : class
  {
    IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate);
    void Create(T odj);
    void SaveChanges();
    T? FirstOrDefault(Expression<Func<T, bool>>? predicate);
  }
}
