﻿using BookStoreManagement.Application.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.DTOs.GenreDtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<BookDto> Books { get; set; } = new();
    }
}
