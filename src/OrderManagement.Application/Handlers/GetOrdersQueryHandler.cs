using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Handlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(request.CustomerName, request.StartDate, request.EndDate);

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerName = o.CustomerName,
                OrderDate = o.OrderDate,
                TotalValue = o.TotalValue,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            });
        }
    }
} 