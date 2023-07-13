using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IResourceAllocationRepository
{
    void DeleteAllocation(int id);
    int AddAllocation(ServiceResource allocation);
    IQueryable<ServiceResource> GetAllAllocations();
    ServiceResource GetAllocation(int id);
    void UpdateAllocation(ServiceResource allocation);
    IQueryable<ServiceResource> GetResourceAllocations(int resourceId);
    IQueryable<ServiceResource> GetServiceAllocations(int serviceId);
    public double GetMaxQuantity(int resourceId);
    public double GetAllocatedQuantity(int resourceId, DateTime startDate, DateTime endDate);
}