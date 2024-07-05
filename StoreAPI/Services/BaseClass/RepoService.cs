using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Linq.Expressions;
using StoreAPI.Database;
using StoreAPI.Interfaces;

namespace StoreAPI.Services.BaseClass
{
  public class RepoService<T> : IRepo<T> where T : class
  {

    public readonly StoreDbContext _context;

    public RepoService(StoreDbContext context)
    {
      _context = context;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate)
    {
      if (predicate == null)
        return _context.Set<T>();

      return _context.Set<T>().Where(predicate);
    }

    public void Create(T odj)
    {
      _context.Set<T>().Add(odj);
    }

    public void SaveChanges()
    {
      _context.SaveChanges();
    }

    public T? FirstOrDefault(Expression<Func<T, bool>>? predicate)
    {
      if (predicate == null)
        return _context.Set<T>().FirstOrDefault();

      return _context.Set<T>().FirstOrDefault(predicate);
    }
  }
}
