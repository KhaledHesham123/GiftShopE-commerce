
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Data.DBContexts;
using System;

namespace ProductCatalogService
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
            builder.Services.AddDbContext<UserProfileDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("UserProfileDatabase"));
            });

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _dbcontext = service.GetRequiredService<UserProfileDbContext>();
            try
            {
                _dbcontext.Database.Migrate();

            }
            catch (Exception ex)
            {

                var logger = service.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An Error Occurred During Apply the Migration");
            }


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
