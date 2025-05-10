using AutoMapper;
using BookStoreManagement.Application.AutoMapping;
using BookStoreManagement.Application.Interfaces;
using BookStoreProject.Domain.Entities;
using BookStoreProject.Domain.Interfaces;
using BookStoreProject.Infrastructure.EfCore.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManagement.Application.Services
{
    public class CrudManager<TEntity, TDto, TCreateDto, TUpdateDto> :
        ICrudService<TEntity, TDto, TCreateDto, TUpdateDto> where TEntity : Entity
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IMapper Mapper;
        protected readonly IValidator<TCreateDto> CreateValidator;
        protected readonly IValidator<TUpdateDto> UpdateValidator;

        public CrudManager()
        {
            Repository = new EfCoreRepository<TEntity>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = config.CreateMapper();

            CreateValidator = TCreateValidator<TCreateDto>();
            UpdateValidator=TCreateValidator<TUpdateDto>();
        }

        public virtual TDto Add(TCreateDto createDto)
        {
            var result = CreateValidator.Validate(createDto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            var entity = Mapper.Map<TEntity>(createDto);
            var addedEntity = Repository.Add(entity);

            return Mapper.Map<TDto>(addedEntity);
        }

        public virtual TDto Delete(int id)
        {
            var exist = Repository.GetById(id);

            if (exist == null)
                throw new InvalidOperationException("not found");

            var deletedEntity = Repository.Delete(exist);

            return Mapper.Map<TDto>(deletedEntity);
        }

        public virtual TDto Get(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var entity = Repository.Get(predicate, asNoTracking, include);

            if (entity == null)
                throw new InvalidOperationException("Not found");

            return Mapper.Map<TDto>(entity);
        }

        public virtual List<TDto> GetAll(Expression<Func<TEntity, bool>>? predicate = null, bool asNoTracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var entities=Repository.GetAll(predicate, asNoTracking, include, orderBy);

            if (entities == null || !entities.Any())
                throw new InvalidOperationException("Not found");

            return Mapper.Map<List<TDto>>(entities);
        }

        public virtual TDto GetById(int id)
        {
            var entity = Repository.GetById(id);

            if (entity == null)
                throw new InvalidOperationException("Not Found");

            return Mapper.Map<TDto>(entity);
        }

        public virtual TDto Update(TUpdateDto updateDto)
        {
            var updatedEntity = Mapper.Map<TEntity>(updateDto);
            var existingEntity = Repository.GetById(updatedEntity.Id);

            if (existingEntity == null)
                throw new InvalidOperationException("Not found");

            return Mapper.Map<TDto>(Repository.Update(updatedEntity));
        }
        
        private IValidator<T> TCreateValidator<T>()
        {
            var validatorType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x=>x.GetTypes()).FirstOrDefault(t=>typeof(IValidator<T>)
                .IsAssignableFrom(t) &&! t.IsAbstract);

            if (validatorType == null)
                throw new Exception($"Invalid type");

            return (IValidator<T>)Activator.CreateInstance(validatorType)!;
        }
    }
}
