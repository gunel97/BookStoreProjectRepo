﻿using BookStoreManagement.Application.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.DTOs.CustomerDtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email {  get; set; }
        public string? Phone { get; set; }
        public List<OrderDto> Orders { get; set; } = new();
    }
}
