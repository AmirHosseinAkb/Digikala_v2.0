namespace DigikalaQuery.Contracts.Order
{
    public interface IOrderQuery
    {
        Tuple<List<OrderQueryModel>,List<OrderQueryModel>,List<OrderQueryModel>> GetUserOrders();
        OrderQueryModel GetOrderDetails(long orderId);
    }
}
