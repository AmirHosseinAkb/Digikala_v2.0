using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;
        private IAuthenticationHelper _authenticationHelper;
        private IShopAccountAcl _shopAccountAcl;

        public InventoryApplication(IInventoryRepository inventoryRepository,IAuthenticationHelper authenticationHelper, IShopAccountAcl shopAccountAcl)
        {
            _inventoryRepository = inventoryRepository;
            _authenticationHelper = authenticationHelper;
            _shopAccountAcl = shopAccountAcl;
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
                ProductId = i.ProductId,
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

        public List<InventoryHistoryViewModel> GetInventoryHistories(long productId)
        {
            var histories = _inventoryRepository.GetInventoryHistories(productId).Select(h =>
                new InventoryHistoryViewModel()
                {
                    OperatorId = h.OperatorId,
                    Count = h.Count,
                    Date = h.Date
                }).ToList();
            histories.ForEach(h =>
            {
                var user = _shopAccountAcl.GetUser(h.OperatorId);
                h.OperatorName = user.fullName;
                h.OperatorRole = user.roleTitle;
            });
            return histories;
        }
    }
}
