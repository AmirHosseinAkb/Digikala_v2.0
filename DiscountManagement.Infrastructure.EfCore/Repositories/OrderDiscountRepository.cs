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
    }
}
