using Domain_Layer.Respones;
using FluentValidation;
using MediatR;

namespace IdentityService.Shared.Behavior
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

                var failures = validators.Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();


                if (failures.Count != 0)
                {
                    var errorMessages = string.Join(" | ", failures.Select(f => f.ErrorMessage));

                    if (IsRequestResponseType(typeof(TRespone)))
                    {
                        var dataType = typeof(TRespone).GetGenericArguments().First();

                        var requestResponseType = typeof(RequestRespones<>).MakeGenericType(dataType);

                        var failMethod = requestResponseType.GetMethod(
                            nameof(RequestRespones<object>.Fail),
                            new[] { typeof(string), typeof(int) });

                        if (failMethod != null)
                        {
                            var failedResponse = failMethod.Invoke(null, new object[] { errorMessages, 400 });

                            return (TRespone)failedResponse;
                        }
                    }

                    throw new ValidationException(failures);
                }
            }


            return await next();

        }


        private bool IsRequestResponseType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(RequestRespones<>);
        }
    }

}

