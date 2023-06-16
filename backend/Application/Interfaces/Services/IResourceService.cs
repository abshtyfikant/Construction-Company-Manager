using Application.DTO.Resource;

namespace Application.Interfaces.Services;

public interface IResourceService
{
    int AddResource(NewResourceDto resource);
    List<ResourceDto> GetResourcesForList();
    object GetResource(int resourceId);
    object UpdateResource(NewResourceDto newResource);
    void DeleteResource(int resourceId);
    void ChangeQuantity(int resourceId, int quantity);
    double GetAvailableQuantityForTime(int id, DateTime startTime, DateTime endTime);
}