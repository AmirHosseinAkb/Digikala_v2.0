
namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Title{ get; set; }
        public int UnitPrice { get; set; }
        public string ImageName{ get; set; }
        public int Count { get; set; }
        public string Brand { get; set; }
        public bool IsInStock { get; set; } = false;
        public long ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
        public int? DiscountRate { get; set; }
        public long DiscountPrice { get; set; }
        public long PayingPrice { get; set; }
        public long TotalItemPrice { get; set; }


        public void CalculateTotalItemPrice()
        {
            TotalItemPrice = Count * UnitPrice;
        }

    }
}
