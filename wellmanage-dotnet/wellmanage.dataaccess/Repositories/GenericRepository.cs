using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wellmanage.data.Data;
using wellmanage.data.Interfaces;

namespace wellmanage.data.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _databaseContext;
        private DbSet<T> _dbSet => _databaseContext.Set<T>();
        public IQueryable<T> Entities => _dbSet;

        public GenericRepository(DataContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void ClearChangeTracker()
        {
            _databaseContext.ChangeTracker.Clear();
        }

        public void DetachState(T entity)
        {
            _databaseContext.Entry(entity).State = EntityState.Detached;
        }

        public void AttachState(T entity)
        {
            _databaseContext.Attach(entity);
            _databaseContext.Entry(entity).State = EntityState.Added;
        }
    }
}