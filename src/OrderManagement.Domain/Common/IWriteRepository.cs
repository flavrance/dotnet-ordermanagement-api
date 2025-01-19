using System;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Common
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
} 