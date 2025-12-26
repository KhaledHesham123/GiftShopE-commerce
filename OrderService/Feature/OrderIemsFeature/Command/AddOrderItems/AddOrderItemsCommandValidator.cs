using FluentValidation;

namespace OrderService.Feature.OrderIemsFeature.Command.AddOrderItems
{
    public class AddOrderItemsCommandValidator:AbstractValidator<AddOrderItemsCommand>
    {
        public AddOrderItemsCommandValidator()
        {
            RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId is required");

            RuleFor(x => x.OrderItems)
                .NotNull()
                .WithMessage("OrderItems cannot be null")
                .Must(items => items.Any())
                .WithMessage("OrderItems cannot be empty");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemDtoValidator());
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("ProductName is required")
                .MaximumLength(200)
                .WithMessage("ProductName must not exceed 200 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero");
        }
    }
}
