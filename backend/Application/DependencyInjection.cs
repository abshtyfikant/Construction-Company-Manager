using System.Reflection;
using Application.Interfaces.Authentication;
using Application.Interfaces.Reports;
using Application.Interfaces.Services;
using Application.Services;
using Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAssignmentService, AssignmentService>();
        services.AddScoped<IResourceAllocationService, ResourceAllocationService>();
        services.AddScoped<IResourceService, ResourceService>();
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