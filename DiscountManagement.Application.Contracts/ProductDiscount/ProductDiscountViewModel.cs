namespace DiscountManagement.Application.Contracts.ProductDiscount
{
    public class ProductDiscountViewModel
    {
        public long DiscountId { get; set; }
        public string ProductTitle { get; set; }
        public int Rate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
