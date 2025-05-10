using BookStoreManagement.Application.DTOs.CustomerDtos;
using BookStoreManagement.Application.DTOs.OrderDetailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public List<OrderDetailDto> OrderDetails = new();
    }
}
