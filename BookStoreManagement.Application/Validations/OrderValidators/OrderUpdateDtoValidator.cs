using BookStoreManagement.Application.DTOs.OrderDtos;
using FluentValidation;

namespace BookStoreManagement.Application.Validations.OrderValidators
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(order => order.Id).NotEmpty().GreaterThan(0);
            RuleFor(order => order.CustomerId).NotEmpty();
            RuleFor(order => order.BookIds).NotEmpty();
            RuleFor(order => order.BookQuantities).NotEmpty();
            RuleFor(order => order.OrderDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(order => order.RequiredDate).NotEmpty().GreaterThan(DateTime.Now.AddDays(3));


        }
    }
}
