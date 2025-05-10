using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;

namespace BookStoreProject.Infrastructure.EfCore.Repositories
{
    public class OrderRepository:EfCoreRepository<Order>, IOrderRepository
    {
        public OrderRepository() : base() { }
    }
}

