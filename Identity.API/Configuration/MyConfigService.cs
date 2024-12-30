using Identity.BLL.Services;
using Identity.Common.Helpers;
using Identity.Common.Mappers;
using Identity.DAL;
using Identity.DAL.Entities;
using Identity.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace Identity.API.Configuration
{
    #region --Inject services--
    public static class MyConfigService
    {
        public static IServiceCollection AddMyDependencyGroup(
             this IServiceCollection services, ConfigurationManager configuration)
        {
            // database
            services.AddDbContext<AppDbContext>((options) =>
                    options.UseSqlServer(configuration.GetConnectionString("IdentityDbConnection")));
            // user
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();
            // permission
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<PermissionRepository>();
            services.AddScoped<PermissionService>();
            // role
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<RoleService>();
            // authentication
            services.AddScoped<AuthenticationService>();
            // Invalidate token
            services.AddScoped<InvalidatedTokenRepository>();

            // hash password
            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));

            // Đăng ký AutoMapper
            services.AddAutoMapper(typeof(PermissionMapper), typeof(UserMapper), typeof(RoleMapper));

            // AppSettings
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // Authentication (validation)
            var Secret = configuration["AppSettings:Secret"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, 
                    option =>
                    {
                        option.TokenValidationParameters = new TokenValidationParameters
                        {
                            // tu cap token
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,

                            // ky vao token
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),

                            ClockSkew = TimeSpan.Zero
                        };

                        // Xử lý khi token không hợp lệ hoặc hết hạn
                        option.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                context.NoResult();
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                return context.Response.WriteAsync("Invalid token.");
                            },
                            OnTokenValidated = context =>
                            {
                                // Thực hiện logic sau khi token được xác thực
                                return Task.CompletedTask;
                            }
                        };

                    }
                );

            // Authorization policy
            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminOrUser", policy => policy.RequireRole("ADMIM","USER"));
            });

            services.AddScoped<IAuthorizationMiddlewareResultHandler, CustomAuthorize>();

            return services;
        }
    }
    #endregion

    #region --Swagger group by http method--
    public class GroupByHttpMethodOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var httpMethod = context.ApiDescription.HttpMethod;

            var entityName = context.ApiDescription.ActionDescriptor.RouteValues["controller"];

            if (!string.IsNullOrEmpty(httpMethod) && !string.IsNullOrEmpty(entityName))
            {
                // Gán tag với định dạng: "HTTP_METHOD - EntityName"
                operation.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag { Name = $"{httpMethod.ToUpper()} - {entityName}" }
                };
            }
        }
    }
    #endregion
}
