using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreProject.Infrastructure.EfCore.Repositories
{
    public class BookRepository : EfCoreRepository<Book>, IBookRepository
    {
        public BookRepository() : base() { }
    }
}

