using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class ResourceAllocationRepository : IResourceAllocationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ResourceAllocationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void DeleteAllocation(int id)
    {
        var allocation = _dbContext.ResourceAllocations.Find(id);
        if (allocation is not null)
        {
            _dbContext.ResourceAllocations.Remove(allocation);
            _dbContext.SaveChanges();
        }
    }

    public int AddAllocation(ServiceResource allocation)
    {
        if (!_dbContext.Resources.Any(s => s.Id == allocation.ResourceId))
            throw new Exception("Resource not found");

        if (!_dbContext.Services.Any(s => s.Id == allocation.ServiceId))
            throw new Exception("Service not found");

        _dbContext.ResourceAllocations.Add(allocation);
        _dbContext.SaveChanges();
        return allocation.Id;
    }

    public IQueryable<ServiceResource> GetAllAllocations()
    {
        var allocations = _dbContext.ResourceAllocations;
        return allocations;
    }

    public ServiceResource GetAllocation(int id)
    {
        var allocation = _dbContext.ResourceAllocations.FirstOrDefault(i => i.Id == id);
        return allocation;
    }

    public double GetMaxQuantity(int resourceId)
    {
        if (!_dbContext.Resources.Any(s => s.Id == resourceId))
            throw new Exception("Resource not found");
        var max = _dbContext.Resources.FirstOrDefault(i => i.Id == resourceId).Quantity;
        return max;
    }

    public void UpdateAllocation(ServiceResource allocation)
    {
        _dbContext.Attach(allocation);
        _dbContext.Entry(allocation).Property("StartDate").IsModified = true;
        _dbContext.Entry(allocation).Property("EndDate").IsModified = true;
        _dbContext.Entry(allocation).Property("AllocatedQuantity").IsModified = true;
        _dbContext.SaveChanges();
    }

    public IQueryable<ServiceResource> GetResourceAllocations(int resourceId)
    {
        if (!_dbContext.Resources.Any(s => s.Id == resourceId))
            throw new Exception("Resource not found");
        var allocations = _dbContext.ResourceAllocations.Where(i => i.ResourceId == resourceId);
        return allocations;
    }

    public IQueryable<ServiceResource> GetServiceAllocations(int serviceId)
    {
        if (!_dbContext.Services.Any(s => s.Id == serviceId))
            throw new Exception("Service not found");
        var allocations = _dbContext.ResourceAllocations.Where(i => i.ServiceId == serviceId);
        return allocations;
    }

    public double GetAllocatedQuantity(int resourceId, DateTime startDate, DateTime endDate)
    {
        if (!_dbContext.Resources.Any(s => s.Id == resourceId))
            throw new Exception("Resource not found");
        var allocations = _dbContext.ResourceAllocations.Where(i => i.ResourceId == resourceId);
        var allocatedQuantity = 0.0;
        foreach (var allocation in allocations)
        {
            if (allocation.BeginDate <= startDate && allocation.EndDate >= endDate)
                allocatedQuantity += allocation.AllocatedQuantity;
        }

        return allocatedQuantity;
    }
}