namespace BookStoreManagement.Application.DTOs.BookDtos
{
    public class BookUpdateDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int PublishedYear { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
