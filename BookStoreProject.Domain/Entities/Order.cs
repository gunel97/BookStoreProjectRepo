namespace BookStoreProject.Domain.Entities
{
    public class Order : Entity
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public required DateTime RequiredDate { get; set; }
        public Customer Customer { get; set; } = null!;
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }

}
