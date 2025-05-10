using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.Interfaces
{
    public interface ICrudService<TEntity, TDto, TCreateDto, TUpdateDto>
    {
        TDto GetById(int id);
        TDto Add(TCreateDto entity);
        TDto Update(TUpdateDto entity);
        TDto Delete(int id); 
        TDto Get(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>>? include = null);
        List<TDto> GetAll(Expression<Func<TEntity, bool>>? predicate = null, bool asNoTracking = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    }
}
