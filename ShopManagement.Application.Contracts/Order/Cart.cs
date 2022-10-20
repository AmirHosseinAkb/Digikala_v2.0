namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public long TotalCartPrice { get; set; }
        public long TotalProductDiscounts { get; set; }
        public long? TotalOrderDiscount { get; set; }
        public long RemainingPrice { get; set; }
        public long? AddressId { get; set; }
        public byte? PaymentType { get; set; }
        public long? OrderDiscountId { get; set; }


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
