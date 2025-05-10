using BookStoreManagement.Application.DTOs.GenreDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.Validations.GenreValidators
{
    public class GenreCreateDtoValidator:AbstractValidator<GenreCreateDto>
    {
        public GenreCreateDtoValidator()
        {
            RuleFor(genre => genre.Name).NotEmpty();
        }
    }
}
