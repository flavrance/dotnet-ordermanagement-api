using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Common
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
} 