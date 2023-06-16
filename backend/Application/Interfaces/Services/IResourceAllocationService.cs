using Application.DTO.ResourceAllocation;

namespace Application.Interfaces.Services;

public interface IResourceAllocationService
{
    void DeleteResourceAllocation(int id);
    int AddResourceAllocation(NewResourceAllocationDto resourceAllocation);
    List<ResourceAllocationDto> GetAllResourceAllocations();
    object GetResourceAllocation(int id);
    object UpdateResourceAllocation(UpdateResourceAllocationDto resourceAllocation);
    List<ResourceAllocationDto> GetResourceAllocationsByResourceId(int resourceId);
    List<ResourceAllocationDto> GetResourceAllocationsByServiceId(int serviceId);
}
