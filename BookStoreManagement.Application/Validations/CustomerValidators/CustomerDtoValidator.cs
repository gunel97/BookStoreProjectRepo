using BookStoreManagement.Application.DTOs.CustomerDtos;
using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.Validations.CustomerValidators
{
     public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateDtoValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty();
            RuleFor(customer => customer.LastName).NotEmpty();
            RuleFor(customer => customer.Phone).NotEmpty().Length(13);
            RuleFor(customer => customer.Email).NotEmpty().EmailAddress();
        }
    }

}
