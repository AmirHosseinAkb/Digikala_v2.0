using _01_Framework.Application;
using DigikalaQuery.Contracts.Services;
using DiscountManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contracts.Order;

namespace DigikalaQuery.Queries
{
    public class DiscountService:IDiscountService
    {
        private readonly DiscountContext _discountContext;

        public DiscountService(DiscountContext discountContext)
        {
            _discountContext = discountContext;
        }
        public OperationResult ConfirmDiscount(string discountCode, Cart cart)
        {
            var result = new OperationResult();
            var discount = _discountContext.OrderDiscounts.FirstOrDefault(d => d.DiscountCode==discountCode);
            if (discount == null)
                return result.Failed(ApplicationMessages.DiscountNotFound);
            else if (discount.StartDate > DateTime.Now)
                return result.Failed(ApplicationMessages.DiscountNotStarted);
            else if (discount.EndDate < DateTime.Now)
                return result.Failed(ApplicationMessages.DiscountExpired);
            else if (discount.UsableCount == 0)
                return result.Failed(ApplicationMessages.DiscountFinished);
            else
            {
                var discountRate = discount.DiscountRate;
                cart.OrderDiscountId=discount.DiscountId;
                cart.TotalOrderDiscount = (cart.RemainingPrice * discountRate) / 100;
                cart.RemainingPrice -= cart.TotalOrderDiscount;
            }
            return result.Succeeded(ApplicationMessages.DiscountConfirmed);
        }
    }
}
