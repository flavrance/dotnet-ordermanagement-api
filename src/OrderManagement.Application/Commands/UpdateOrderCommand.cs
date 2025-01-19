using System;
using System.Collections.Generic;
using MediatR;

namespace OrderManagement.Application.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
} 