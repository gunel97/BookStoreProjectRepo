﻿namespace BookStoreManagement.Application.DTOs.CustomerDtos
{
    public class CustomerCreateDto
    {
        public required string FirstName {  get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

    }
}
