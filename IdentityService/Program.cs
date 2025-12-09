
using FluentValidation;
using IdentityService.Data;
using IdentityService.Features.Authantication;
using IdentityService.Features.Authantication.Commands.Login;
using IdentityService.Shared.Behavior;
using IdentityService.Shared.Configurations;
using IdentityService.Shared.Repository;
using IdentityService.Shared.Services.EmailVerificationServices;
using IdentityService.Shared.UIitofwork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityService
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

            

            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IunitofWork, UnitofWork>();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEMailSettings, EMailSettings>();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginCommend).Assembly);
            });

            builder.Services.AddValidatorsFromAssemblyContaining<LoginCommendValidator>();

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));




            var app = builder.Build();


            app.MapAuthanticationEndpoints();

            app.UseStaticFiles();

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _dbcontext = service.GetRequiredService<ApplicationDbcontext>();
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
