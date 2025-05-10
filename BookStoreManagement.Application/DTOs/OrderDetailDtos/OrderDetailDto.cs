using BookStoreManagement.Application.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.DTOs.OrderDetailDtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string BookTitle {  get; set; }
        public int Quantity { get; set; }
        public double TotalPrice {  get; set; }
    }
}
