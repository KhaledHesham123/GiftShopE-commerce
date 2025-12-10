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
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
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

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionHandler>();

            app.MapAuthanticationEndpoints();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbcontext>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
