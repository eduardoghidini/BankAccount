using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankAccount.Warren.Domain.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> ListAllAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetByIdAsync(int id);
    }
}