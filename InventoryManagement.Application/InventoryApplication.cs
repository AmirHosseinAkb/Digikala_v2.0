using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public Tuple<List<InventoryViewModel>, int, int, int> GetInventories(int pageId = 1,long productId=0, bool isInStock = false,int take=10)
        {
            var inventories = _inventoryRepository.GetInventories(productId, isInStock);
            var products = _inventoryRepository.GetProducts();
            var query = inventories.Select(i => new InventoryViewModel()
            {
                ProductCount = i.ProductCount,
                InventoryId = i.InventoryId,
                ProductId = i.ProductId
            }).ToList();
            query.ForEach(i =>
            {
                i.ProductPrice = products.FirstOrDefault(p => p.Item1 == i.ProductId).Item2;
                i.ProductTitle = products.FirstOrDefault(p => p.Item1 == i.ProductId).Item3;
                i.ImageName = products.FirstOrDefault(p => p.Item1 == i.ProductId).Item4;
            });

            var skip = (pageId - 1) * take;

            var pageCount = query.Count() / take;

            if (query.Count() % take != 0)
                pageCount++;

            return Tuple.Create(query, pageId, pageCount, take);
        }
    }
}
