namespace BookStoreProject.Domain.Entities
{
    public class Book:Entity
    {
        public required string Title { get; set; }
        public string Description { get; set; } = null!;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public required int PublishedYear {  get; set; }
        public required double Price { get; set; }
        public required int QuantityInStock { get; set; }
        public Genre Genre { get; set; } = null!;
        public Author Author { get; set; } = null!;
        public List<OrderDetail> OrderDetails { get; set; } = new();

    }

}
