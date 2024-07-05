using System.Linq.Expressions;

namespace WebApplication1.Interfaces
{
  public interface IRepo<T> where T : class
  {
    IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate);
    void Create(T odj);
    void SaveChanges();
    T? FirstOrDefault(Expression<Func<T, bool>>? predicate);
  }
}
