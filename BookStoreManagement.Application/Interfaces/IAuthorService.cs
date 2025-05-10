using BookStoreManagement.Application.DTOs.AuthorDtos;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Interfaces
{
    public interface IAuthorService:ICrudService<Author, AuthorDto, AuthorCreateDto, AuthorUpdateDto>
    {

    }
}
