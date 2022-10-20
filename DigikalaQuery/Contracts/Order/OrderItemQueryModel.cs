namespace DigikalaQuery.Contracts.Order
{
    public class OrderItemQueryModel
    {
        public string Title { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public int UnitPrice { get; set; }
        public int Count { get; set; }
        public int TotalPrice { get; set; }
        public string ImageName { get; set; }
    }
}
