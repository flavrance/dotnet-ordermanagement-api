using System;
using System.Collections.Generic;
using OrderManagement.Domain.Common;

namespace OrderManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerName { get; private set; }
        public DateTime OrderDate { get; private set; }
        private readonly List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public decimal TotalValue => CalculateTotalValue();
        public OrderStatus Status { get; private set; }

        public Order(string customerName) : base()
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));

            CustomerName = customerName;
            OrderDate = DateTime.UtcNow;
            _items = new List<OrderItem>();
            Status = OrderStatus.Pending;
        }

        public void AddItem(string name, int quantity, decimal unitPrice)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify a non-pending order.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            
            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));

            var item = new OrderItem(name, quantity, unitPrice);
            _items.Add(item);
            SetModified();
        }

        public void UpdateCustomerName(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));

            CustomerName = customerName;
            SetModified();
        }

        public void RemoveItem(Guid itemId)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify a non-pending order.");

            var item = _items.Find(i => i.Id == itemId);
            if (item != null)
            {
                _items.Remove(item);
                SetModified();
            }
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            if (Status == newStatus)
                return;

            if (Status == OrderStatus.Cancelled || Status == OrderStatus.Completed)
                throw new InvalidOperationException("Cannot change status of a cancelled or completed order.");

            Status = newStatus;
            SetModified();
        }

        private decimal CalculateTotalValue()
        {
            decimal total = 0;
            foreach (var item in _items)
            {
                total += item.Quantity * item.UnitPrice;
            }
            return total;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }
} 