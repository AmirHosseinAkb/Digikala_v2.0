namespace InventoryManagement.Domain.InventoryHistoryAgg
{
    public class InventoryHistory
    {
        public long InventoryHistoryId { get; private set; }
        public long ProductId { get; private set; }
        public long OperatorId { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}
