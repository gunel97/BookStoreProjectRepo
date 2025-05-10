namespace BookStoreManagement.Application.DTOs.BookDtos
{
    public class BookCreateDto
    {
        public required string Title {  get; set; }
        public string Description { get; set; } = null!;
        public required int PublishedYear { get; set; }
        public required int QuantityInStock { get; set; }
        public required double Price { get; set; }
        public required int AuthorId {  get; set; }
        public required int GenreId { get; set; }
    }
}
