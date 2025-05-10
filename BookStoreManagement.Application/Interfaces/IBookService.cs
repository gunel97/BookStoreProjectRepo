using BookStoreManagement.Application.DTOs.BookDtos;
using BookStoreProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.Interfaces
{
    public interface IBookService:ICrudService<Book, BookDto, BookCreateDto, BookUpdateDto>
    {
    }
}
