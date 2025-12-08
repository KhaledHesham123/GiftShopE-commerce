using MediatR;

namespace ProductCatalogService.Shared.Extenstions
{
    public static class GenericHandlerRegistrationExtensions
    {
        public static IServiceCollection AddGenericHandlers(
            this IServiceCollection services,
            List<Type> entityTypes,
            Type queryOpenType,
            Type handlerOpenType,
            Type responseType,
            params Type[] extraGenericArgs)
        {
            if (!queryOpenType.IsGenericTypeDefinition)
                throw new ArgumentException("queryOpenType must be an open generic type, e.g. CheckExistQuery<>");

            if (!handlerOpenType.IsGenericTypeDefinition)
                throw new ArgumentException("handlerOpenType must be an open generic type, e.g. CheckExistQueryHandler<>");

            var serviceOpenType = typeof(IRequestHandler<,>);

            foreach (var entityType in entityTypes)
            {
                var queryArgs = new List<Type> { entityType };
                if (extraGenericArgs != null && extraGenericArgs.Length > 0)
                    queryArgs.AddRange(extraGenericArgs);

                var queryClosed = queryOpenType.MakeGenericType(queryArgs.ToArray());
                var serviceType = serviceOpenType.MakeGenericType(queryClosed, responseType);
                var handlerType = handlerOpenType.MakeGenericType(queryArgs.ToArray());

                services.AddTransient(serviceType, handlerType);
            }

            return services;
        }
    }
}
