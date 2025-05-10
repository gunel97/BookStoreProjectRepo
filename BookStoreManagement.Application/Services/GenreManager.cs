using BookStoreManagement.Application.DTOs.GenreDtos;
using BookStoreManagement.Application.Interfaces;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Services
{
    public class GenreManager : CrudManager<Genre, GenreDto, GenreCreateDto, GenreUpdateDto>, IGenreService { }
}
