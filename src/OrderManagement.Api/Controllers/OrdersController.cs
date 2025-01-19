using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Commands;
using OrderManagement.Application.DTOs;
using OrderManagement.Application.Queries;

namespace OrderManagement.Api.Controllers
{
    /// <summary>
    /// Controller for managing orders in the system
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the OrdersController
        /// </summary>
        /// <param name="mediator">MediatR instance for handling commands and queries</param>
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="command">Order creation details</param>
        /// <returns>The ID of the created order</returns>
        /// <response code="201">Returns the newly created order's ID</response>
        /// <response code="400">If the command is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
        }

        /// <summary>
        /// Gets all orders with optional filtering
        /// </summary>
        /// <param name="customerName">Optional customer name filter</param>
        /// <param name="startDate">Optional start date filter</param>
        /// <param name="endDate">Optional end date filter</param>
        /// <returns>List of orders matching the criteria</returns>
        /// <response code="200">Returns the list of orders</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders(
            [FromQuery] string? customerName,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var query = new GetOrdersQuery
            {
                CustomerName = customerName,
                StartDate = startDate,
                EndDate = endDate
            };

            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        /// <summary>
        /// Gets an order by its ID
        /// </summary>
        /// <param name="id">The ID of the order to retrieve</param>
        /// <returns>The order details</returns>
        /// <response code="200">Returns the requested order</response>
        /// <response code="404">If the order is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery { Id = id };
            var order = await _mediator.Send(query);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        /// <summary>
        /// Updates an existing order
        /// </summary>
        /// <param name="id">The ID of the order to update</param>
        /// <param name="command">The updated order details</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the update was successful</response>
        /// <response code="400">If the ID doesn't match the command</response>
        /// <response code="404">If the order is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <param name="id">The ID of the order to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the deletion was successful</response>
        /// <response code="404">If the order is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
} 