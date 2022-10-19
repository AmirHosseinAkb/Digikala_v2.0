using DiscountManagement.Application.Contracts.OrderDiscount;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.Discount
{
    public class ShopDiscountAcl:IShopDiscountAcl
    {
        private readonly IOrderDiscountApplication _orderDiscountApplication;

        public ShopDiscountAcl(IOrderDiscountApplication orderDiscountApplication)
        {
            _orderDiscountApplication = orderDiscountApplication;
        }
        public void ReduceDiscountUsableCount(long discountId)
        {
            _orderDiscountApplication.ReduceDiscountUsableCount(discountId);
        }
    }
}
