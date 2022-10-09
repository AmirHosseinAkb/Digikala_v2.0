namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public long TotalCartPrice { get; set; }
        public long TotalProductDiscounts { get; set; } = 0;
        public long TotalOrderDiscount { get; set; } = 0;
        public long RemainingPrice { get; set; }
        public long AddressId { get; set; } = 0;
        public byte PaymentType { get; set; } = 0;
        public long OrderDiscountId { get; set; } = 0;

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public void Add(CartItem cartItem)
        {
            CartItems.Add(cartItem);
            if (cartItem.IsInStock)
            {
                TotalCartPrice += cartItem.TotalItemPrice;
                TotalProductDiscounts += cartItem.DiscountPrice;
                RemainingPrice += cartItem.PayingPrice;
            }
            
        }
    }
}
