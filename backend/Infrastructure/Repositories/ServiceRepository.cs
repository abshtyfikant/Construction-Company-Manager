using Application.DTO.ResourceAllocation;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ServiceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int AddService(Service service, List<Assignment> assignments, List<ServiceResource> resources, List<Material> materials)
    {
        if (!_dbContext.Clients.Any(s => s.Id == service.ClientId)) throw new Exception("Client not found");
        _dbContext.Services.Add(service);

        foreach (var material in materials)
        {
            material.ServiceId = service.Id;
            material.Service = service;
            _dbContext.Materials.Add(material);
        }

        foreach (var resource in resources)
        {
            resource.ServiceId = service.Id;
            resource.Service = service;
            _dbContext.ResourceAllocations.Add(resource);
        }

        foreach (var assignment in assignments)
        {
            Console.WriteLine(assignment.Employee);
            assignment.ServiceId = service.Id;
            assignment.Service = service;
            _dbContext.Assignments.Add(assignment);
        }

        _dbContext.SaveChanges();
        return service.Id;
    }

    public void DeleteService(int serviceId)
    {
        var service = _dbContext.Services.Find(serviceId);
        if (service is not null)
        {
            _dbContext.Services.Remove(service);
            _dbContext.SaveChanges();
        }
    }

    public IQueryable<Service> GetAllServices()
    {
        var services = _dbContext.Services;
        return services;
    }

    public Service GetService(int serviceId)
    {
        var service = _dbContext.Services.FirstOrDefault(i => i.Id == serviceId);
        return service;
    }

    public void UpdateService(Service service, List<Assignment> assignments, List<ServiceResource> resources, List<Material> materials)
    {//najprawodopodobniej trzeba dorobic usuwanie jesli nie ma materialu/zasobu/pracownika ktory byl a w tym requescie go nie ma
        _dbContext.Attach(service);
        _dbContext.Entry(service).Property("ServiceType").IsModified = true;
        _dbContext.Entry(service).Property("BeginDate").IsModified = true;
        _dbContext.Entry(service).Property("EndDate").IsModified = true;
        _dbContext.Entry(service).Property("ServiceStatus").IsModified = true;
        _dbContext.Entry(service).Property("PaymentStatus").IsModified = true;
        _dbContext.Entry(service).Property("City").IsModified = true;

        foreach (var material in materials)
        {
            material.ServiceId = service.Id;
            material.Service = service;
            var materialToUpdate = _dbContext.Materials.Find(material.Id);

            if (materialToUpdate != null)
            {
                materialToUpdate.Name = material.Name;
                materialToUpdate.Unit = material.Unit;
                materialToUpdate.Price = material.Price;
                materialToUpdate.Quantity = material.Quantity;
            }
            else
            {
                _dbContext.Materials.Add(material);
            }
        }

        foreach (var resource in resources)
        {
            resource.ServiceId = service.Id;
            resource.Service = service;
            var resourceToUpdate = _dbContext.ResourceAllocations.Find(resource.Id);

            if (resourceToUpdate != null)
            {
                resourceToUpdate.BeginDate = resource.BeginDate;
                resourceToUpdate.EndDate = resource.EndDate;
                resourceToUpdate.AllocatedQuantity = resource.AllocatedQuantity;
            }
            else
            {
                _dbContext.ResourceAllocations.Add(resource);
            }
        }

        foreach (var assignment in assignments)
        {
            assignment.ServiceId = service.Id;
            assignment.Service = service;
            var assignmentToUpdate = _dbContext.Assignments.Find(assignment.Id);
            if (assignmentToUpdate != null)
            {
                assignmentToUpdate.Function = assignment.Function;
                assignmentToUpdate.EmployeeId = assignment.EmployeeId;
                assignmentToUpdate.ServiceId = assignment.ServiceId;
            }
            else
            {
                _dbContext.Assignments.Add(assignment);
            }
        }

        _dbContext.SaveChanges();
    }
}