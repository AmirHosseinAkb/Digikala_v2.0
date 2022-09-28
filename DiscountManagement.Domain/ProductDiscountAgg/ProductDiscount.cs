namespace DiscountManagement.Domain.ProductDiscountAgg;

public class ProductDiscount
{
    public long ProductDiscountId { get; private set; }
    public long ProductId { get; private set; }
    public int Rate { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsDeleted { get; private set; }

    protected ProductDiscount()
    {

    }

    public ProductDiscount(long productId, int rate, DateTime startDate, DateTime endDate)
    {
        ProductId = productId;
        Rate = rate;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Edit(long productId, int rate, DateTime startDate, DateTime endDate)
    {
        ProductId = productId;
        Rate = rate;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Delete() => IsDeleted = true;
}