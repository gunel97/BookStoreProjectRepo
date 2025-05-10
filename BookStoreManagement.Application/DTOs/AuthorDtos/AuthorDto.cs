using BookStoreManagement.Application.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.DTOs.AuthorDtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public List<BookDto> Books { get; set; } = new();
    }
}
