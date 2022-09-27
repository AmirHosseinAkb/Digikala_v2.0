using _01_Framework.Application;
using DiscountManagement.Domain.ProductDiscountAgg;

namespace DiscountManagement.Infrastructure.EfCore.Repositories;

public class ProductDiscountRepository:IProductDiscountRepository
{
    private readonly DiscountContext _context;

    public ProductDiscountRepository(DiscountContext context)
    {
        _context = context;
    }
    public IQueryable<ProductDiscount> GetProductDiscounts(long productId = 0, string startDate = "", string endDate = "")
    {
        IQueryable<ProductDiscount> discounts=_context.ProductDiscounts;
        if (productId != 0)
            discounts = discounts.Where(d => d.ProductId == productId);
        if (!string.IsNullOrEmpty(startDate))
        {
            startDate.ShamsiToGerogorian();
            discounts = discounts.Where(d => d.StartDate == DateTime.Parse(startDate));
        }

        if (!string.IsNullOrEmpty(endDate))
        {
            endDate.ShamsiToGerogorian();
            discounts=discounts.Where(d => d.EndDate == DateTime.Parse(endDate));
        }
        return discounts;
    }
}