using BookStoreManagement.Application.DTOs.BookDtos;
using BookStoreManagement.Application.DTOs.OrderDtos;
using BookStoreManagement.Application.Interfaces;
using BookStoreProject.Domain.Entities;

namespace BookStoreManagement.Application.Services
{
    public class OrderManager : CrudManager<Order, OrderDto, OrderCreateDto, OrderUpdateDto>, IOrderService
    {
        public override OrderDto Add(OrderCreateDto createDto)
        {
            var bookService = new BookManager();

            Order order = new Order
            {
                CustomerId = createDto.CustomerId,
                OrderDate = createDto.OrderDate,
                RequiredDate = createDto.RequiredDate,

            };

            for (int i = 0; i < createDto.BookIds.Count; i++)
            {
                var oDetail = new OrderDetail();
                var book = bookService.GetById(createDto.BookIds[i]);
                oDetail.BookId = createDto.BookIds[i];
                oDetail.Quantity = createDto.BookQuantities[i];
                oDetail.TotalPrice = book.Price * createDto.BookQuantities[i];
                order.OrderDetails.Add(oDetail);
            }

            return Mapper.Map<OrderDto>(Repository.Add(order));
        }
    }
}
