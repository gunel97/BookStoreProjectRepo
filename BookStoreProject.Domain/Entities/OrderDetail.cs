namespace BookStoreProject.Domain.Entities
{
    public class OrderDetail : Entity
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice {  get; set; }
        public Book Book { get; set; } = null!;
        public Order Order { get; set; } = null!;

    }

}
