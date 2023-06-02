using Application.Interfaces.Authentication;
using Application.Interfaces.Services;
using Domain.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {

            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            return services;
        }
    }
}
