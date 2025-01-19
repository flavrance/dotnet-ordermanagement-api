using System;
using System.Collections.Generic;

namespace OrderManagement.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 