using AutoMapper;
using BookStoreManagement.Application.DTOs.AuthorDtos;
using BookStoreManagement.Application.DTOs.BookDtos;
using BookStoreManagement.Application.DTOs.CustomerDtos;
using BookStoreManagement.Application.DTOs.GenreDtos;
using BookStoreManagement.Application.DTOs.OrderDetailDtos;
using BookStoreManagement.Application.DTOs.OrderDtos;
using BookStoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.AutoMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(x=>x.FullName, opt=>opt.MapFrom(src=>src.FirstName+" "+src.LastName)).ReverseMap();
            CreateMap<Author, AuthorCreateDto>().ReverseMap();
            CreateMap<Author, AuthorUpdateDto>().ReverseMap();

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Genre, GenreCreateDto>().ReverseMap();
            CreateMap<Genre, GenreUpdateDto>().ReverseMap();

            CreateMap<Book, BookDto>()
                .ForMember(x=>x.AuthorName, opt=>opt.MapFrom(src=>src.Author.FirstName+" "+src.Author.LastName))
                .ForMember(x=>x.GenreName, opt=>opt.MapFrom(src=>src.Genre.Name)).ReverseMap();
            CreateMap<Book, BookCreateDto>().ReverseMap();
            CreateMap<Book, BookUpdateDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>()
                .ForMember(x=>x.FullName, opt=>opt.MapFrom(src=>src.FirstName+" "+src.LastName)).ReverseMap();
            CreateMap<Customer, CustomerCreateDto>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDto>().ReverseMap();


            CreateMap<Order, OrderDto>()
                .ForMember(x=>x.CustomerName, opt=>opt.MapFrom(src=>src.Customer.FirstName+" "+src.Customer.LastName)).ReverseMap();
            
            CreateMap<Order, OrderCreateDto>().ReverseMap();

            //CreateMap<OrderCreateDto, Order>()
            //    .ForMember(x => x.OrderDetails, opt => opt.MapFrom(src => src.BookIds.Select
            //    (x => new OrderDetail { BookId = x })))
            //    .ForMember(x => x.OrderDetails, opt => opt.MapFrom(src => src.BookQuantities.Select
            //    (x => new OrderDetail { Quantity = x }))).ReverseMap();

            CreateMap<OrderCreateDto, Order>()
                .ForMember(x => x.OrderDetails, opt => opt.MapFrom(src => src.BookIds.Select
                (x => new OrderDetail { BookId = x }))).ReverseMap();


            CreateMap<Order, OrderUpdateDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(x=>x.BookTitle, opt=>opt.MapFrom(src=>src.Book.Title)).ReverseMap();
               

        }
    }
}
