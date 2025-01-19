using System;
using System.Collections.Generic;
using MediatR;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public string? CustomerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
} 