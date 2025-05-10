using BookStoreManagement.Application.DTOs.BookDtos;
using BookStoreManagement.Application.Interfaces;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Services
{
    public class BookManager:CrudManager<Book, BookDto, BookCreateDto, BookUpdateDto>, IBookService { }
}
