using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;

namespace BookStoreProject.Infrastructure.EfCore.Repositories
{
    public class AuthorRepository : EfCoreRepository<Author>, IAuthorRepository
    {
        public AuthorRepository() : base() { }
    }
}

