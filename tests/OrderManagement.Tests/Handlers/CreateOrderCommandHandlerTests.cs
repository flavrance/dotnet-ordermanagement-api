using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Handlers;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Tests.Handlers
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _handler = new CreateOrderCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateOrder()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerName = "Test Customer",
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                        Name = "Test Item",
                        Quantity = 1,
                        UnitPrice = 10.00m
                    }
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyCustomerName_ShouldThrowException()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerName = string.Empty,
                Items = new List<OrderItemDto>()
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
} 