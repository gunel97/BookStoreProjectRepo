using BookStoreManagement.Application.DTOs.AuthorDtos;
using FluentValidation;

namespace BookStoreManagement.Application.Validations.AuthorValidators
{
    public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
    {
        public AuthorUpdateDtoValidator()
        {
            RuleFor(author=>author.Id).NotEmpty().GreaterThan(0);
            RuleFor(author => author.FirstName).NotEmpty();
        }
    }
}
