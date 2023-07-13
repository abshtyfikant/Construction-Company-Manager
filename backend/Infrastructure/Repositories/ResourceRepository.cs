using Domain.Interfaces.Repository;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ResourceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void DeleteResource(int id)
    {
        var resource = _dbContext.Resources.Find(id);
        var allocations = _dbContext.ResourceAllocations.Where(x => x.ResourceId == id);
        if (!allocations.IsNullOrEmpty())
        {
            throw new Exception("Resource is allocated");
        }   
        if (resource is not null)
        {
            _dbContext.Resources.Remove(resource);
            _dbContext.SaveChanges();
        }
    }

    public int AddResource(Resource resource)
    {
        _dbContext.Resources.Add(resource);
        _dbContext.SaveChanges();
        return resource.Id;
    }

    public void UpdateResource(Resource resource)
    {
        if (!_dbContext.Resources.Any(s => s.Id == resource.Id)) throw new Exception("Resource not Found");

        _dbContext.Attach(resource);
        _dbContext.Entry(resource).Property("Name").IsModified = true;
        _dbContext.Entry(resource).Property("Quantity").IsModified = true;
        _dbContext.SaveChanges();
    }

    public Resource GetResource(int id)
    {
        var resource = _dbContext.Resources.FirstOrDefault(x => x.Id == id);
        return resource;
    }

    public IQueryable<Resource> GetAll()
    {
        var resources = _dbContext.Resources;
        return resources;
    }

    public void ChangeQuantity(int id, int quantity)
    {
        if (!_dbContext.Resources.Any(s => s.Id == id)) throw new Exception("Resource not Found");

        var resource = _dbContext.Resources.Find(id);
        if (resource is not null)
        {
            _dbContext.Attach(resource);
            _dbContext.Entry(resource).Property("Quantity").IsModified = true;
            _dbContext.SaveChanges();
        }
    }

    public double GetAvailableQuantityForTime(int id, DateTime startTime, DateTime endTime)
    {
        if (!_dbContext.Resources.Any(s => s.Id == id)) throw new Exception("Resource not Found");
        var resource = _dbContext.Resources.Find(id);
        var availableQuantity = resource.Quantity;
        var allocations = _dbContext.ResourceAllocations.Where(x => x.ResourceId == id);
        foreach (var allocation in allocations)
        {
            if (allocation.BeginDate <= startTime && allocation.EndDate >= endTime)
                availableQuantity -= allocation.AllocatedQuantity;
        }
        return availableQuantity;
    }
}