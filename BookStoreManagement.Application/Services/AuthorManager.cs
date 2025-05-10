using BookStoreManagement.Application.DTOs.AuthorDtos;
using BookStoreManagement.Application.Interfaces;
using BookStoreProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.Services
{
    public class AuthorManager:CrudManager<Author, AuthorDto, AuthorCreateDto, AuthorUpdateDto>, IAuthorService
    {
    }
}
