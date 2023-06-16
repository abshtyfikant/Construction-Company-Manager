using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IResourceRepository
{
    void DeleteResource(int id);
    int AddResource(Resource resource);
    void UpdateResource(Resource resource);
    Resource GetResource(int id);
    IQueryable<Resource> GetAll();
    void ChangeQuantity(int id, int quantity);
    double GetAvailableQuantityForTime(int id, DateTime startTime, DateTime endTime);
}