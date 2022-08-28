namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory
    {
        public long InventoryId { get; private set; }
        public long ProductId { get; private set; }
        public int ProductCount { get; set; }
    }
}
