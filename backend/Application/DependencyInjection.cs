using Application.Interfaces.Authentication;
using Application.Interfaces.Reports;
using Application.Interfaces.Services;
using Application.Services;
using Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
