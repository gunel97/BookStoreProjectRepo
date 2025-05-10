using BookStoreManagement.Application.DTOs.GenreDtos;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Interfaces
{
    public interface IGenreService:ICrudService<Genre, GenreDto, GenreCreateDto, GenreUpdateDto> { }
}
