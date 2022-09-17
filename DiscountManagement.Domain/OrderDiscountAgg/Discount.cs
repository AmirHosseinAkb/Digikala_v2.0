namespace DiscountManagement.Domain.OrderDiscountAgg;

public class OrderDiscount
{
    public long DiscountId { get; private set; }
    public string Code { get; private set; }
    public int Percent { get;private set; }
    public string Reason { get;private set; }
    public int? UsableCount { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public OrderDiscount(string code, int percent, string reason, int? usableCount, DateTime? startDate, DateTime? endDate)
    {
        Code = code;
        Percent = percent;
        Reason = reason;
        UsableCount = usableCount;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Edit(string code, int percent, string reason, int? usableCount, DateTime? startDate, DateTime? endDate)
    {
        Code = code;
        Percent = percent;
        Reason = reason;
        UsableCount = usableCount;
        StartDate = startDate;
        EndDate = endDate;
    }
}