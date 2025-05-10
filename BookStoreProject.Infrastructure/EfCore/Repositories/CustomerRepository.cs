using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;

namespace BookStoreProject.Infrastructure.EfCore.Repositories
{
    public class CustomerRepository : EfCoreRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository() : base() { }
    }
}

