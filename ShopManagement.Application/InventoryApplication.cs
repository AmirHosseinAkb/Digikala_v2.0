using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;
        private IAuthenticationHelper _authenticationHelper;

        public InventoryApplication(IInventoryRepository inventoryRepository,IAuthenticationHelper authenticationHelper)
        {
            _inventoryRepository = inventoryRepository;
            _authenticationHelper = authenticationHelper;
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

        public OperationResult ChangeInventory(ChangeInventoryCommand command)
        {
            var result = new OperationResult();
            var inventory = _inventoryRepository.GetInventry(command.InventoryId);
            if (inventory == null)
                return result.Failed(ApplicationMessages.RecordNotFound);
            if (command.IsForDecrease)
            {
                if (inventory.ProductCount < command.Count)
                    return result.Failed(ApplicationMessages.CantDecreaseInventory);
                command.Count *= -1;
            }
            inventory.ChangeInventory(command.Count);
            var inventoryHistory = new InventoryHistory(inventory.ProductId, _authenticationHelper.GetCurrentUserId(),
                command.Count);
            _inventoryRepository.AddInventoryHistory(inventoryHistory);
            return result.Succeeded();
        }
    }
}
