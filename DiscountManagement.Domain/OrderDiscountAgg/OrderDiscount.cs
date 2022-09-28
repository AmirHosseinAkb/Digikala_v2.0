namespace DiscountManagement.Domain.OrderDiscountAgg;

public class OrderDiscount
{
    public long DiscountId { get; private set; }
    public string DiscountCode { get; private set; }
    public int DiscountRate { get;private set; }
    public string Reason { get;private set; }
    public int? UsableCount { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsDeleted { get; private set; }

    public OrderDiscount(string discountCode, int discountRate, string reason, int? usableCount, DateTime? startDate, DateTime? endDate)
    {
        DiscountCode = discountCode;
        DiscountRate = discountRate;
        Reason = reason;
        UsableCount = usableCount;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Edit(string discountCode, int discountRate, string reason, int? usableCount, DateTime? startDate, DateTime? endDate)
    {
        DiscountCode = discountCode;
        DiscountRate = discountRate;
        Reason = reason;
        UsableCount = usableCount;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Delete()=> IsDeleted = true;
    
}