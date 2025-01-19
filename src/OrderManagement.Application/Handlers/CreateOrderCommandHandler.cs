using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.CustomerName);

            foreach (var item in request.Items)
            {
                order.AddItem(item.Name, item.Quantity, item.UnitPrice);
            }

            await _orderRepository.AddAsync(order);

            return order.Id;
        }
    }
} 