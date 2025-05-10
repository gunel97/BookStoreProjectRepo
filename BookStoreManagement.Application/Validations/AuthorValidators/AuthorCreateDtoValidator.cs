using BookStoreManagement.Application.DTOs.AuthorDtos;
using FluentValidation;

namespace BookStoreManagement.Application.Validations.AuthorValidators
{
    public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
    {
        public AuthorCreateDtoValidator()
        {
              RuleFor(author=>author.FirstName).NotEmpty();
        }
    }
}
