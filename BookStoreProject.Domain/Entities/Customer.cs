namespace BookStoreProject.Domain.Entities
{
    public class Customer : Entity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
       public List<Order> Orders { get; set; } = new();
    }

}
