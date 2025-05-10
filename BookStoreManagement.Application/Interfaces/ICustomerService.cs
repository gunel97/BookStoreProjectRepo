using BookStoreManagement.Application.DTOs.CustomerDtos;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Interfaces
{
    public interface ICustomerService:ICrudService<Customer, CustomerDto, CustomerCreateDto, CustomerUpdateDto> { }
}
