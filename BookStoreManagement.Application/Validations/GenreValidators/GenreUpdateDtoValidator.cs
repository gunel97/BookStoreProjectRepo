using BookStoreManagement.Application.DTOs.GenreDtos;
using FluentValidation;

namespace BookStoreManagement.Application.Validations.GenreValidators
{
    public class GenreUpdateDtoValidator : AbstractValidator<GenreUpdateDto>
    {
        public GenreUpdateDtoValidator()
        {
            RuleFor(genre => genre.Id).NotEmpty().GreaterThan(0);
            RuleFor(genre => genre.Name).NotEmpty();
        }
    }
}
