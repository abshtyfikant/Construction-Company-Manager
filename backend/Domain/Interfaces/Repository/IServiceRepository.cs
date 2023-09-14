using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IServiceRepository
{
    void DeleteService(int serviceId);
    int AddService(Service service, List<Assignment> assignments, List<ServiceResource> resources, List<Material> materials);
    IQueryable<Service> GetAllServices();
    Service GetService(int serviceId);
    void UpdateService(Service service, List<Assignment> assignments, List<ServiceResource> resources, List<Material> materials);
    double GetServiceEarnings(DateTime start, DateTime end);
    double GetServiceCost(int serviceId);
}
