using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;

namespace InventoryManagement.Infrastructure.EfCore.Repositories;

public class InventoryRepository:IInventoryRepository
{
    private readonly InventoryContext _inventoryContext;
    private readonly ShopContext _shopContext;

    public InventoryRepository(InventoryContext inventoryContext,ShopContext shopContext)
    {
        _inventoryContext=inventoryContext;
        _shopContext=shopContext;
    }
    public List<Inventory> GetInventories(long productId=0, bool isInStock = false)
    {
        IQueryable <Inventory> inventories= _inventoryContext.Inventories;
        if (productId != 0)
            inventories = inventories.Where(i => i.ProductId == productId);

        if (isInStock)
            inventories = inventories.Where(p => p.ProductCount > 0);
        return inventories.ToList();
    }

    public List<Tuple<long,int,string,string>> GetProducts()
    {
        return _shopContext.Products.Select(p => Tuple.Create(p.ProductId,p.Price ,p.Title, p.ImageName)).ToList();
    }
}