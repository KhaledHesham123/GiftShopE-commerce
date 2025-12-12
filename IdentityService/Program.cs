using FluentValidation;
using IdentityService.Data;
using IdentityService.Data.DataSeed;
using IdentityService.Features.Authantication;
using IdentityService.Features.Authantication.Commands.Login;
using IdentityService.Features.Authantication.Commands.SignUp;
using IdentityService.Shared.Behavior;
using IdentityService.Shared.Configurations;
using IdentityService.Shared.Middlewares;
using IdentityService.Shared.Repository;
using IdentityService.Shared.Services;
using IdentityService.Shared.Services.EmailVerificationServices;
using IdentityService.Shared.UIitofwork;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("identityConnection"));
                //options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitofWork, UnitofWork>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEMailSettings, EMailSettings>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
            });

            builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<SignUpCommandValidator>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  var issuer = builder.Configuration["JWT:Issuer"];
                  var Audience = builder.Configuration["JWT:Audience"];
                  var key = builder.Configuration["JWT:Key"];
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = issuer,
                      ValidAudience = Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                  };
              });
            builder.Services.AddAuthorization();
            builder.Services.AddMemoryCache();
            builder.Services.AddTransient<GlobalExceptionHandler>();
            builder.Services.AddMassTransit( X =>
            {
                X.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration["RabbitMQ:Host"], h =>
                    {
                        h.Username(builder.Configuration["RabbitMQ:Username"]);
                        h.Password(builder.Configuration["RabbitMQ:Password"]);
                    });
                    cfg.ConfigureEndpoints(context); //for all Consumers
                });
            });

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionHandler>();

            app.MapAuthanticationEndpoints();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var context = services.GetRequiredService<ApplicationDbcontext>();
                    var retryCount = 0;
                    var maxRetries = 10;

                    while (retryCount < maxRetries)
                    {

                        try
                        {
                            await DataSeeder.SeedAsync(context);
                            break;
                        }
                        catch (Exception ex)
                        {
                            retryCount++;
                            logger.LogWarning($"Waiting for SQL Server... attempt {retryCount}/10");

                            if (retryCount == maxRetries)
                                throw;

                            await Task.Delay(3000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    // Tell Swagger UI where to find the OpenAPI document
                    options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
                });
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
