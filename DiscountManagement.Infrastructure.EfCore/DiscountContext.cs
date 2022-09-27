using DiscountManagement.Domain.OrderDiscountAgg;
using DiscountManagement.Domain.ProductDiscountAgg;
using DiscountManagement.Infrastructure.EfCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EfCore;

public class DiscountContext:DbContext
{
    public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
    {
        
    }

    public DbSet<OrderDiscount> OrderDiscounts { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderDiscountMapping());
        modelBuilder.ApplyConfiguration(new ProductDiscountMapping());
        base.OnModelCreating(modelBuilder);
    }
}