using System;
using Xunit;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Tests.Domain
{
    public class OrderTests
    {
        [Fact]
        public void CreateOrder_WithValidData_ShouldSucceed()
        {
            // Arrange & Act
            var order = new Order("John Doe");

            // Assert
            Assert.NotEqual(Guid.Empty, order.Id);
            Assert.Equal("John Doe", order.CustomerName);
            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.Empty(order.Items);
            Assert.Equal(0, order.TotalValue);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null!)]
        public void CreateOrder_WithInvalidCustomerName_ShouldThrowException(string customerName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Order(customerName));
        }

        [Fact]
        public void AddItem_WithValidData_ShouldSucceed()
        {
            // Arrange
            var order = new Order("John Doe");

            // Act
            order.AddItem("Test Item", 2, 10.00m);

            // Assert
            Assert.Single(order.Items);
            Assert.Equal(20.00m, order.TotalValue);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AddItem_WithInvalidQuantity_ShouldThrowException(int quantity)
        {
            // Arrange
            var order = new Order("John Doe");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => order.AddItem("Test Item", quantity, 10.00m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AddItem_WithInvalidPrice_ShouldThrowException(decimal price)
        {
            // Arrange
            var order = new Order("John Doe");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => order.AddItem("Test Item", 1, price));
        }

        [Fact]
        public void AddItem_ToNonPendingOrder_ShouldThrowException()
        {
            // Arrange
            var order = new Order("John Doe");
            order.UpdateStatus(OrderStatus.Processing);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.AddItem("Test Item", 1, 10.00m));
        }

        [Fact]
        public void UpdateStatus_FromPendingToProcessing_ShouldSucceed()
        {
            // Arrange
            var order = new Order("John Doe");

            // Act
            order.UpdateStatus(OrderStatus.Processing);

            // Assert
            Assert.Equal(OrderStatus.Processing, order.Status);
        }

        [Fact]
        public void UpdateStatus_CompletedOrder_ShouldThrowException()
        {
            // Arrange
            var order = new Order("John Doe");
            order.UpdateStatus(OrderStatus.Completed);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.UpdateStatus(OrderStatus.Processing));
        }

        [Fact]
        public void RemoveItem_FromPendingOrder_ShouldSucceed()
        {
            // Arrange
            var order = new Order("John Doe");
            order.AddItem("Test Item", 1, 10.00m);
            var item = order.Items.First();

            // Act
            order.RemoveItem(item.Id);

            // Assert
            Assert.Empty(order.Items);
            Assert.Equal(0, order.TotalValue);
        }

        [Fact]
        public void RemoveItem_FromNonPendingOrder_ShouldThrowException()
        {
            // Arrange
            var order = new Order("John Doe");
            order.AddItem("Test Item", 1, 10.00m);
            var item = order.Items.First();
            order.UpdateStatus(OrderStatus.Processing);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.RemoveItem(item.Id));
        }
    }
} 