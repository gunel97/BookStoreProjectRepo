using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;
using BookStoreProject.Infrastructure.EfCore.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreProject.Infrastructure.EfCore.Repositories
{
    public class EfCoreRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext Context;
        public EfCoreRepository()
        {
            Context = new AppDbContext();
        }
        public virtual T Add(T entity)
        {
            var added = Context.Set<T>().Add(entity);
            Context.SaveChanges();

            return added.Entity;
        }

        public T Delete(T entity)
        {
            var deleted = Context.Set<T>().Remove(entity);
            Context.SaveChanges();

            return deleted.Entity;
        }

        public T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = Context.Set<T>();

            query=query.Where(predicate);

            if (!asNoTracking)
                query = query.AsNoTracking();

            if(include!=null)
                query=include.Invoke(query);

            return query.FirstOrDefault() ??
                throw new InvalidOperationException("Entity not found");
        }

        public List<T> GetAll(Expression<Func<T, bool>>? predicate = null, bool asNoTracking = false, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
           IQueryable<T>  query = Context.Set<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include.Invoke(query);

            if(!asNoTracking)
                query=query.AsNoTracking();

            if (orderBy != null)
                query = orderBy.Invoke(query);

            return query.ToList();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().AsNoTracking().SingleOrDefault(x => x.Id == id) ??
                throw new InvalidOperationException("Entity not found");
        }

        public T Update(T entity)
        {
            var updated = Context.Set<T>().Update(entity);
            Context.SaveChanges(); 
            
            return updated.Entity;
        }
    }
}
