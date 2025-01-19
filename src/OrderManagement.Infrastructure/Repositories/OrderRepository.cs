using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string? customerName, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Orders.AsQueryable(); // Inicia a consulta como uma consulta vazia

            // Se customerName for fornecido, adiciona o filtro para o nome do cliente
            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(o => o.CustomerName.Contains(customerName));
            }

            // Se startDate e endDate forem fornecidos, adiciona o filtro para o intervalo de datas
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= startDate.Value && o.OrderDate <= endDate.Value);
            }
            // Se apenas startDate for fornecido, adiciona o filtro para a data de início
            else if (startDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= startDate.Value);
            }
            // Se apenas endDate for fornecido, adiciona o filtro para a data de término
            else if (endDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= endDate.Value);
            }

            // Inclui os itens do pedido (se necessário)
            query = query.Include(o => o.Items);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomerNameAsync(string customerName)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.CustomerName.Contains(customerName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        
    }
} 