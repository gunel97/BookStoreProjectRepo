namespace BookStoreManagement.Application.DTOs.OrderDtos
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public List<int> BookIds { get;set; } = new();
        public List<int> BookQuantities { get; set; } = new();
    }
}
