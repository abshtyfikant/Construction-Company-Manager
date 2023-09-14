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
        _dbContext.SaveChanges();

        foreach (var material in materials)
        {
            material.ServiceId = service.Id;
            _dbContext.Materials.Add(material);
        }

        foreach (var resource in resources)
        {
            resource.ServiceId = service.Id;
            _dbContext.ResourceAllocations.Add(resource);
        }

        foreach (var assignment in assignments)
        {
            assignment.ServiceId = service.Id;
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
    {
        _dbContext.Attach(service);
        _dbContext.Entry(service).Property("ServiceType").IsModified = true;
        _dbContext.Entry(service).Property("BeginDate").IsModified = true;
        _dbContext.Entry(service).Property("EndDate").IsModified = true;
        _dbContext.Entry(service).Property("ServiceStatus").IsModified = true;
        _dbContext.Entry(service).Property("PaymentStatus").IsModified = true;
        _dbContext.Entry(service).Property("City").IsModified = true;
        _dbContext.Entry(service).Property("Price").IsModified = true;

        var oldMaterials = _dbContext.Materials.Where(m => m.ServiceId == service.Id).ToList();
        var oldResources = _dbContext.ResourceAllocations.Where(r => r.ServiceId == service.Id).ToList();
        var oldAssignments = _dbContext.Assignments.Where(a => a.ServiceId == service.Id).ToList();

        // Delete old materials, resources and assignments
        foreach (var material in oldMaterials.Where(material => materials.All(m => m.Id != material.Id)))
        {
            this._dbContext.Materials.Remove(material);
        }

        foreach (var resource in oldResources.Where(resource => resources.All(r => r.Id != resource.Id)))
        {
            this._dbContext.ResourceAllocations.Remove(resource);
        }

        foreach (var assignment in oldAssignments.Where(assignment => assignments.All(a => a.Id != assignment.Id)))
        {
            this._dbContext.Assignments.Remove(assignment);
        }

        // Add new materials, resources and assignments
        foreach (var material in materials.Where(material => oldMaterials.All(m => m.Id != material.Id)))
        {
            material.ServiceId = service.Id;
            this._dbContext.Materials.Add(material);
        }

        foreach (var resource in resources.Where(resource => oldResources.All(r => r.Id != resource.Id)))
        {
            resource.ServiceId = service.Id;
            this._dbContext.ResourceAllocations.Add(resource);
        }

        foreach (var assignment in assignments.Where(assignment => oldAssignments.All(a => a.Id != assignment.Id)))
        {
            assignment.ServiceId = service.Id;
            this._dbContext.Assignments.Add(assignment);
        }

        // Update existing materials, resources and assignments
        foreach (var material in materials.Where(material => oldMaterials.Any(m => m.Id == material.Id)))
        {
            this._dbContext.Attach(material);
            this._dbContext.Entry(material).Property("Name").IsModified = true;
            this._dbContext.Entry(material).Property("Quantity").IsModified = true;
            this._dbContext.Entry(material).Property("Unit").IsModified = true;
            this._dbContext.Entry(material).Property("Price").IsModified = true;
        }

        foreach (var resource in resources.Where(resource => oldResources.Any(r => r.Id == resource.Id)))
        {
            this._dbContext.Attach(resource);
            this._dbContext.Entry(resource).Property("EmployeeId").IsModified = true;
            this._dbContext.Entry(resource).Property("BeginDate").IsModified = true;
            this._dbContext.Entry(resource).Property("EndDate").IsModified = true;
            this._dbContext.Entry(resource).Property("AllocatedQuantity").IsModified = true;
        }

        foreach (var assignment in assignments.Where(assignment => oldAssignments.Any(a => a.Id == assignment.Id)))
        {
            this._dbContext.Attach(assignment);
            this._dbContext.Entry(assignment).Property("EmployeeId").IsModified = true;
            this._dbContext.Entry(assignment).Property("BeginDate").IsModified = true;
            this._dbContext.Entry(assignment).Property("EndDate").IsModified = true;
            this._dbContext.Entry(assignment).Property("Function").IsModified = true;
        }

        _dbContext.SaveChanges();
    }

    public double GetServiceEarnings(DateTime start, DateTime end)
    {
        var services = _dbContext.Services.Where(s => s.BeginDate >= start && s.EndDate <= end);
        var earnings = services.Sum(s => s.Price);
        return (double)earnings;
    }

    public double GetServiceCost(int serviceId)
    {
        var costs = 0.0;
        var service = _dbContext.Services.Find(serviceId);
        var materials = _dbContext.Materials.Where(m => m.ServiceId == serviceId).ToList();
        var assignments = _dbContext.Assignments.Where(a => a.ServiceId == serviceId).ToList();

        var materialCost = materials.Sum(m => (double)m.Price * m.Quantity);
        costs += materialCost;

        foreach (var assignment in assignments)
        {
            var employee = _dbContext.Employees.Find(assignment.EmployeeId);
            if (employee is null) continue;
            var daysAssigned = new List<DateTime>();
            for (var date = assignment.StartDate; date <= assignment.EndDate; date = date.AddDays(1))
            {
                daysAssigned.Add(date);
            }
            var workDays = daysAssigned.Where(i => i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday).Count();
            costs += workDays * 8 * employee.RatePerHour;
        }

        return costs;
    }
}
