using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.InventoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class InventoryRepository:IInventoryRepository
    {
        private readonly ShopContext _shopContext;

        public InventoryRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public List<Inventory> GetAll(string title = "", bool isInStock = false)
        {
            IQueryable<Inventory> inventories = _shopContext.Inventories.Include(i=>i.Product);
            if (!string.IsNullOrEmpty(title))
                inventories = inventories.Where(i => i.Product.Title.Contains(title));
            if (isInStock)
                inventories = inventories.Where(i => i.ProductCount > 0);
            return inventories.ToList();
        }

        public Inventory? GetInventry(long id)
        {
            return _shopContext.Inventories.Find(id);
        }

        public void AddInventoryHistory(InventoryHistory history)
        {
            _shopContext.InventoryHistories.Add(history);
            _shopContext.SaveChanges();
        }
    }
}
