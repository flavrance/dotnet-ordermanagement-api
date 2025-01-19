using System;
using MediatR;

namespace OrderManagement.Application.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid Id { get; set; }
    }
} 