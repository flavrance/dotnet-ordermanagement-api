using System;
using OrderManagement.Domain.Common;

namespace OrderManagement.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public Guid OrderId { get; private set; }

        public OrderItem(string name, int quantity, decimal unitPrice) : base()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Item name cannot be empty.", nameof(name));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));

            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            Quantity = quantity;
            SetModified();
        }

        public void UpdateUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));

            UnitPrice = unitPrice;
            SetModified();
        }

        public void SetOrder(Guid orderId)
        {
            OrderId = orderId;
            SetModified();
        }
    }
} 