using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
       public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
       public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
       public Task<T> AddAsync(T entity);
       public Task UpdateAsync(int id,T entity);
        public Task<bool> RemoveAsync(int id);
        public Task SaveAsync();

    }
}



