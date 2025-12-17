using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Data.DBContexts;
using UserProfileService.Data.Seeders;
using UserProfileService.Features.OccasionFeatures;
using UserProfileService.Features.OccasionFeatures.Add.AddOccasion.Dto;
using UserProfileService.Features.OccasionFeatures.Add.AddOccasionQr;
using UserProfileService.Features.Shared;
using UserProfileService.Features.Shared.Queries.CheckExist;
using UserProfileService.Features.Shared.Queries.GetByCriteria;
using UserProfileService.Shared.Behaviors;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Extenstions;
using UserProfileService.Shared.Helper;
using UserProfileService.Shared.Hup;
using UserProfileService.Shared.Interfaces;
using UserProfileService.Shared.Middlewares;
using UserProfileService.Shared.Repositories;

namespace UserProfileService
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

            builder.Services.AddSignalR();
            builder.Services.AddDbContext<ProductCatalogDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProductCatalogDatabase"));
            });
            builder.Services.AddScoped<IAddOccasionQr, AddOccasionQr>();
            builder.Services.AddScoped<IImageHelper, ImageHelper>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddMediatR(typeof(Program).Assembly);

            builder.Services.AddCors(option => option.AddPolicy("myPolicy", option =>
            {
                option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            builder.Services.AddHttpContextAccessor();
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
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapOccasionEndpoints();

            // Seed
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ProductCatalogDbContext>();
                    await DataSeeder.SeedAsync(context);
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
            app.UseMiddleware<ExceptionHandlingMiddleware>();
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
