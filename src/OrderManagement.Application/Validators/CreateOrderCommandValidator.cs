using FluentValidation;
using OrderManagement.Application.Commands;

namespace OrderManagement.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required")
                .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Item name is required")
                    .MaximumLength(200).WithMessage("Item name cannot exceed 200 characters");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero");

                item.RuleFor(x => x.UnitPrice)
                    .GreaterThan(0).WithMessage("Unit price must be greater than zero");
            });
        }
    }
} 