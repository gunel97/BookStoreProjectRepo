using BookStoreManagement.Application.DTOs.CustomerDtos;
using BookStoreManagement.Application.Interfaces;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Services
{
    public class CustomerManager:CrudManager<Customer, CustomerDto, CustomerCreateDto, CustomerUpdateDto>, ICustomerService { }
}
