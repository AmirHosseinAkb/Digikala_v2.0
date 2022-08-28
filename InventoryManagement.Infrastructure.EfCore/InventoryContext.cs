using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Domain.InventoryHistoryAgg;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.EfCore;

public class InventoryContext:DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options):base(options)
    {
        
    }

    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryHistory> InventoryHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
}