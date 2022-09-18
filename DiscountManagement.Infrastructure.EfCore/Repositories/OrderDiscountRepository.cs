using DiscountManagement.Domain.OrderDiscountAgg;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EfCore.Repositories
{
    public class OrderDiscountRepository:IOrderDiscountRepository
    {
        private readonly DiscountContext _context;

        public OrderDiscountRepository(DiscountContext context)
        {
            _context = context;
        }
        public void Add(OrderDiscount orderDiscount)
        {
            _context.OrderDiscounts.Add(orderDiscount);
            _context.SaveChanges();
        }

        public bool IsExistDiscount(string code)
        {
            return _context.OrderDiscounts.Any(d => d.DiscountCode == code);
        }

        public IQueryable<OrderDiscount> GetOrderDiscounts(string code = "", string reason = "", bool isActive = false)
        {
            IQueryable<OrderDiscount> discounts = _context.OrderDiscounts;
            if(!string.IsNullOrEmpty(code))
                discounts = discounts.Where(d => d.DiscountCode.Contains(code));

            if(!string.IsNullOrEmpty(reason))
                discounts = discounts.Where(d => d.Reason.Contains(reason));
            if (isActive)
                discounts = discounts.Where(d =>(d.StartDate==null || d.StartDate <= DateTime.Now) && (d.EndDate==null || d.EndDate >= DateTime.Now));
            
            return discounts;
        }

        public OrderDiscount GetOrderDiscount(long discountId)
        {
            return _context.OrderDiscounts.Find(discountId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
