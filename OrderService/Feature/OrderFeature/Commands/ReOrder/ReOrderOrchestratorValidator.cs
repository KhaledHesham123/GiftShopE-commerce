using FluentValidation;

namespace OrderService.Feature.OrderFeature.Commands.ReOrder
{
    public class ReOrderOrchestratorValidator:AbstractValidator<ReOrderOrchestrator>
    {
        public ReOrderOrchestratorValidator()
        {
            RuleFor(x => x.OrderId)
               .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one item must be provided.");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                     .NotEmpty().WithMessage("ProductId is required.");

                items.RuleFor(i => i.Quantity)
                     .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            });

           
        }
    }
}
