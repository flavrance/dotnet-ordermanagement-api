using System;
using Xunit;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Tests.Domain
{
    public class OrderItemTests
    {
        [Fact]
        public void CreateOrderItem_WithValidData_ShouldSucceed()
        {
            // Arrange & Act
            var item = new OrderItem("Test Item", 2, 10.00m);

            // Assert
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Equal("Test Item", item.Name);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(10.00m, item.UnitPrice);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null!)]
        public void CreateOrderItem_WithInvalidName_ShouldThrowException(string name)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new OrderItem(name, 1, 10.00m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateOrderItem_WithInvalidQuantity_ShouldThrowException(int quantity)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new OrderItem("Test Item", quantity, 10.00m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateOrderItem_WithInvalidPrice_ShouldThrowException(decimal price)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new OrderItem("Test Item", 1, price));
        }

        [Fact]
        public void UpdateQuantity_WithValidQuantity_ShouldSucceed()
        {
            // Arrange
            var item = new OrderItem("Test Item", 1, 10.00m);

            // Act
            item.UpdateQuantity(2);

            // Assert
            Assert.Equal(2, item.Quantity);
            Assert.NotNull(item.LastModifiedAt);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UpdateQuantity_WithInvalidQuantity_ShouldThrowException(int quantity)
        {
            // Arrange
            var item = new OrderItem("Test Item", 1, 10.00m);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => item.UpdateQuantity(quantity));
        }

        [Fact]
        public void UpdateUnitPrice_WithValidPrice_ShouldSucceed()
        {
            // Arrange
            var item = new OrderItem("Test Item", 1, 10.00m);

            // Act
            item.UpdateUnitPrice(15.00m);

            // Assert
            Assert.Equal(15.00m, item.UnitPrice);
            Assert.NotNull(item.LastModifiedAt);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UpdateUnitPrice_WithInvalidPrice_ShouldThrowException(decimal price)
        {
            // Arrange
            var item = new OrderItem("Test Item", 1, 10.00m);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => item.UpdateUnitPrice(price));
        }

        [Fact]
        public void SetOrder_ShouldSetOrderIdAndModifiedDate()
        {
            // Arrange
            var item = new OrderItem("Test Item", 1, 10.00m);
            var orderId = Guid.NewGuid();

            // Act
            item.SetOrder(orderId);

            // Assert
            Assert.Equal(orderId, item.OrderId);
            Assert.NotNull(item.LastModifiedAt);
        }
    }
} 