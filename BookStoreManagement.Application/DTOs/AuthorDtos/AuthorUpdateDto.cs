namespace BookStoreManagement.Application.DTOs.AuthorDtos
{
    public class AuthorUpdateDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
    }
}
