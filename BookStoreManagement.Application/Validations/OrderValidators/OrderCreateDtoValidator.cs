using BookStoreManagement.Application.DTOs.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.Validations.OrderValidators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(order => order.CustomerId).NotEmpty();
            RuleFor(order => order.BookIds).NotEmpty();
            RuleFor(order => order.BookQuantities).NotEmpty();
            RuleFor(order=>order.OrderDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(order=>order.RequiredDate).NotEmpty().GreaterThan(DateTime.Now.AddDays(3));

        }
    }
}
