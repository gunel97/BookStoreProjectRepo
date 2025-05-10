using BookStoreManagement.Application.DTOs.AuthorDtos;
using BookStoreManagement.Application.DTOs.GenreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.DTOs.BookDtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
       public AuthorDto Author { get; set; } = null!;
        public string? AuthorName {  get; set; }
        public GenreDto Genre { get; set; } = null!;
        public string? GenreName { get; set; }
        public int PublishedYear { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
