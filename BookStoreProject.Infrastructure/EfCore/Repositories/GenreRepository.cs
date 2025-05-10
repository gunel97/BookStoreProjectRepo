using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;

namespace BookStoreProject.Infrastructure.EfCore.Repositories
{
    public class GenreRepository : EfCoreRepository<Genre>, IGenreRepository
    {
        public GenreRepository() : base() { }
    }
}

