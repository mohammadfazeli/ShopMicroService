using Ordering.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}