using BookStoreManagement.Application.DTOs.BookDtos;
using FluentValidation;

namespace BookStoreManagement.Application.Validations.BookValidators
{
    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto> {
        public BookCreateDtoValidator()
        {
            RuleFor(book=>book.Title).NotEmpty();
            RuleFor(book=>book.Price).NotEmpty().GreaterThan(0);
            RuleFor(book=>book.QuantityInStock).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(book=>book.AuthorId).NotEmpty().GreaterThan(0);
            RuleFor(book=>book.GenreId).NotEmpty().GreaterThan(0);
            RuleFor(book=>book.PublishedYear).NotEmpty().LessThanOrEqualTo(DateTime.Now.Year);
        }

    }


}
