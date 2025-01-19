using System;
using MediatR;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
    }
} 