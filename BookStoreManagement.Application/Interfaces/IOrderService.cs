using BookStoreManagement.Application.DTOs.OrderDtos;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Interfaces
{
    public interface IOrderService:ICrudService<Order, OrderDto, OrderCreateDto, OrderUpdateDto> { }
}
