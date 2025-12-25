using FluentValidation;
using MediatR;
using System.Reflection;

namespace OrderService.Shared.Behavior
{
    public class ValidationBehavior<TRequest, TRespone> : IPipelineBehavior<TRequest, TRespone>
       where TRequest : IRequest<TRespone>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
        public async Task<TRespone> Handle(TRequest request, RequestHandlerDelegate<TRespone> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationTasks = validators.Select(v => v.ValidateAsync(context, cancellationToken));

                var validationResults = await Task.WhenAll(validationTasks);

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();

        }



    }

}

