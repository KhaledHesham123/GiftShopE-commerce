
using CartService.Shared.basketRepository;
using CartService.Shared.Behavior;
using CartService.Shared.MasTranset.Consumers;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Reflection.Metadata.Ecma335;

namespace CartService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddMassTransit(busconfigrator =>
            {
                busconfigrator.SetKebabCaseEndpointNameFormatter();

                busconfigrator.AddConsumer<ProductAddedToCartEventConsumer>();


                busconfigrator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("72.61.102.216", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("g19HBzycmCfePY6MREFm");
                    });
                    cfg.ReceiveEndpoint("cart-product-added-queue", e =>
                    {
                        e.UseMessageRetry(x=>x.Interval(3,TimeSpan.FromSeconds(5)));
                        e.ConfigureConsumer<ProductAddedToCartEventConsumer>(context);
                    });
                });
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            builder.Services.AddScoped<IbasketRepository, BasketRepository>();


            builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
