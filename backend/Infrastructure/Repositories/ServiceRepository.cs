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

    public int AddService(Service service)
    {
        if (!_dbContext.Clients.Any(s => s.Id == service.ClientId)) throw new Exception("Client not found");
        _dbContext.Services.Add(service);
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

    public void UpdateService(Service service)
    {
        _dbContext.Attach(service);
        _dbContext.Entry(service).Property("ServiceType").IsModified = true;
        _dbContext.Entry(service).Property("BeginDate").IsModified = true;
        _dbContext.Entry(service).Property("EndDate").IsModified = true;
        _dbContext.Entry(service).Property("ServiceStatus").IsModified = true;
        _dbContext.Entry(service).Property("PaymentStatus").IsModified = true;
        _dbContext.Entry(service).Property("City").IsModified = true;
        _dbContext.SaveChanges();
    }
}