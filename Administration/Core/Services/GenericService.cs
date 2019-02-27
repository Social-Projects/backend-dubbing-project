using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public abstract class GenericService<T>
        where T : BaseEntity
    {
        protected readonly IRepository<T> Repository;

        protected GenericService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Repository.ListAllAsync();
        }

        public virtual async Task<List<T>> GetAllByCondition(Expression<Func<T, bool>> predicate)
        {
            return await Repository.List(predicate);
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Repository.GetById(id);
        }

        public virtual async Task Create(T entity)
        {
            await Repository.AddAsync(entity);
        }

        public virtual async Task Update(int id, T newEntity)
        {
            var oldEntity = await Repository.GetById(id);

            if (oldEntity == null)
                throw new Exception($"{typeof(T)} entity with ID: {id} doesn't exist.");

            await Repository.UpdateAsync(oldEntity, newEntity);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await Repository.GetById(id);

            if (entity == null)
                return;

            await Repository.DeleteAsync(entity);
        }
    }
}