using System;
using System.Collections.Generic;
using MediatR;

namespace OrderManagement.Application.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public string CustomerName { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 