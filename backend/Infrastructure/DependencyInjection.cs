using Application.Interfaces.Authentication;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddAuth(configuration);
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton(Options.Create(jwtSettings));

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings.Issuer,
                     ValidAudience = jwtSettings.Audience,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(jwtSettings.Secret))
                 });
            return services;
        }
    }
}
