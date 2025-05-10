namespace BookStoreManagement.Application.DTOs.AuthorDtos
{
    public class AuthorCreateDto
    {
        public required string FirstName {  get; set; }
        public string LastName { get; set; } = null!;
    }
}
