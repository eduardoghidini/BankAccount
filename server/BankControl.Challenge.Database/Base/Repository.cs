using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BankAccount.Warren.Database.Contexts;
using BankAccount.Warren.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Warren.Database.Base
{
    public class Repository<Entity> : IRepository<Entity>
        where Entity : class, IEntity
    {
        private readonly BankAccountDbContextFactory _dbFactory;

        private DbSet<Entity> _dbSet;

        protected DbSet<Entity> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<Entity>());
        }

        public Repository(BankAccountDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Add(Entity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(Entity entity)
        {
            DbSet.Remove(entity);
        }

        public Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> expression)
        {
            return DbSet.Where(expression).ToListAsync();
        }

        public void Update(Entity entity)
        {
            DbSet.Update(entity);
        }

        public Task<Entity> GetByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task<int> CountAsync(Expression<Func<Entity, bool>> expression)
        {
            return DbSet.CountAsync(expression);
        }

        public Task<List<Entity>> ListAllAsync()
        {
            return DbSet.ToListAsync();
        }
    }
}