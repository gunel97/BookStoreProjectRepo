using BookStoreManagement.Application.DTOs.CustomerDtos;
using FluentValidation;

namespace BookStoreManagement.Application.Validations.CustomerValidators
{
    public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateDtoValidator()
        {
            RuleFor(customer => customer.Id).NotEmpty().GreaterThan(0);
            RuleFor(customer => customer.FirstName).NotEmpty();
            RuleFor(customer => customer.LastName).NotEmpty();
            RuleFor(customer => customer.Phone).NotEmpty().Length(13);
            RuleFor(customer => customer.Email).NotEmpty().EmailAddress();
        }
    }

}
