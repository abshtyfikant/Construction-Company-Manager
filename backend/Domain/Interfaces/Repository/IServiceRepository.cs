using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IServiceRepository
{
    void DeleteService(int serviceId);
    int AddService(Service service);
    IQueryable<Service> GetAllServices();
    Service GetService(int serviceId);
    void UpdateService(Service service);
}