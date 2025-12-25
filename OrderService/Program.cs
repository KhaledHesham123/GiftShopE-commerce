
using OrderService.Shared.Repository;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.DBContexts;
using OrderService.Shared.basketRepository;
using OrderService.Shared.Behavior;
using OrderService.Shared.Middlewars;
using OrderService.Shared.Repository;
using OrderService.Shared.UIitofwork;
using ProductCatalogService.Shared.basketRepository;
using StackExchange.Redis;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options => 
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((contxt, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin123");
                    });
                });
            });

            builder.Services.AddSignalR();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase"));

            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IbasketRepository, BasketRepository>();


            builder.Services.AddScoped<IUnitofWork, UnitofWork>();
            
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
            
            //builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            builder.Services.AddTransient<GlobalExceptionHandler>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });


            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionHandler>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
