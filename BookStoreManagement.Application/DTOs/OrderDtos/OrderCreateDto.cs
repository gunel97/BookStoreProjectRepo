using BookStoreManagement.Application.DTOs.OrderDetailDtos;

namespace BookStoreManagement.Application.DTOs.OrderDtos
{
    public class OrderCreateDto
    {
        public int CustomerId {  get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public List<OrderDetailDto> OrderDetails = new();
        public List<int> BookIds { get; set; } = new();
        public List<int> BookQuantities { get; set; } = new();


    }
}
