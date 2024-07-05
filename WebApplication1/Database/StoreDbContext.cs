﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Database

{
  public class StoreDbContext : DbContext
  {
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

    #region DbSet

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductOrderMap> ProductOrderMap { get; set; }

    #endregion
  }
}
