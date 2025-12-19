using CartService.Shared.basketRepository;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Data.DBContexts;
using ProductCatalogService.Data.Seeders;
using ProductCatalogService.Features.CartFeature;
using ProductCatalogService.Features.OccasionFeatures;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using ProductCatalogService.Features.OccasionFeatures.Add.AddOccasionQr;
using ProductCatalogService.Features.ProductFeatures;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Features.Shared.Queries.CheckExist;
using ProductCatalogService.Features.Shared.Queries.GetByCriteria;
using ProductCatalogService.Shared.basketRepository;
using ProductCatalogService.Shared.Behaviors;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Extenstions;
using ProductCatalogService.Shared.Helper;
using ProductCatalogService.Shared.Hup;
using ProductCatalogService.Shared.Interfaces;
using ProductCatalogService.Shared.Middlewares;
using ProductCatalogService.Shared.Repositories;
using StackExchange.Redis;

namespace ProductCatalogService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
            builder.Services.AddDbContext<ProductCatalogDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProductCatalogDatabase"));
            });
            builder.Services.AddScoped<IAddOccasionQr, AddOccasionQr>();
            builder.Services.AddScoped<IImageHelper, ImageHelper>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IbasketRepository, BasketRepository>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddMediatR(typeof(Program).Assembly);

            builder.Services.AddCors(option => option.AddPolicy("myPolicy", option =>
            {
                option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            #region Register Generic Handlers dynamically

            var entityAssembly = typeof(Category).Assembly; // adjust if entities in different assembly
            var entityTypes = entityAssembly.GetTypes()
                .Where(t => typeof(BaseEntity).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass)
                .ToList();

            builder.Services.AddGenericHandlers(
                 entityTypes,
                 typeof(CheckExistQuery<>),
                 typeof(CheckExistQueryHandler<>),
                 typeof(Result<bool>));

            builder.Services.AddGenericHandlers(
                entityTypes,
                typeof(GetByCriteriaQuery<,>),
                typeof(GetByCriteriaQueryHandler<,>),
                typeof(Result<Guid>),
                typeof(Guid));
            #endregion
            
            var app = builder.Build();
            //app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapOccasionEndpoints();
            app.MapProductFeatureEndpoints();
            app.MapCartFeatureEndpoints();

            // Seed
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ProductCatalogDbContext>();
                    //await DataSeeder.SeedAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            // Configure the HTTP request pipeline.
            

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCatalog API V1");
                c.RoutePrefix = string.Empty; // يفتح Swagger على /
            });

            app.UseHttpsRedirection();
            app.UseCors("myPolicy");
            app.UseAuthorization();
            app.UseMiddleware<TransactionMiddleware>();
            app.UseMiddleware<SaveChangesMiddleware>();
            
            app.MapHub<OccasionHub>("/hubs/occasionHub");
            app.MapControllers();

            app.Run();
        }
    }
}
