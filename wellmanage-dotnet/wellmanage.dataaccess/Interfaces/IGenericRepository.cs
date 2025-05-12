
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace wellmanage.data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void AttachState(T entity);
        void DetachState(T entity);
        void ClearChangeTracker();
    }
}
