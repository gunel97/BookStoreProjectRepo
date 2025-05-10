namespace BookStoreProject.Domain.Entities
{
    public class Author:Entity
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public List<Book> Books { get; set; } = new();
    }

}
