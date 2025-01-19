using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetAllAsync(string? customerName, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<Order>> GetByCustomerNameAsync(string customerName);
        Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
} 