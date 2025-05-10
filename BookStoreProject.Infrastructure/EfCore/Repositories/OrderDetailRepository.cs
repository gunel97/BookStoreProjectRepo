using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;

namespace BookStoreProject.Infrastructure.EfCore.Repositories
{
    public class OrderDetailRepository : EfCoreRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository() : base() { }
    }
}

