using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.InventoryAgg;

namespace ShopManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public Tuple<List<InventoryViewModel>,int,int,int> GetInventoriesForShow(int pageId, string title = "", bool isInStock = false, int take = 10)
        {
            var inventories = _inventoryRepository.GetAll(title, isInStock);

            var skip = (pageId - 1) / take;
            var pageCount = inventories.Count() / take;

            if (inventories.Count() % take != 0)
                pageCount++;

            var query = inventories.Skip(skip).Take(take).Select(i => new InventoryViewModel()
            {
                InventoryId = i.InventoryId,
                ImageName = i.Product.ImageName,
                ProductTitle = i.Product.Title,
                ProductCount = i.ProductCount,
                ProductPrice = i.Product.Price
            }).ToList();

            return Tuple.Create(query, pageId, pageCount, take);
        }
    }
}
