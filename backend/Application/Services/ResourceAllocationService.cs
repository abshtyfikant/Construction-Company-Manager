using Application.DTO.ResourceAllocation;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

public class ResourceAllocationService : IResourceAllocationService
{
    private readonly IResourceAllocationRepository _resourceAllocationRepository;
    private readonly IMapper _mapper;

    public ResourceAllocationService(IMapper mapper, IResourceAllocationRepository resourceAllocationRepository)
    {
        _mapper = mapper;
        _resourceAllocationRepository = resourceAllocationRepository;
    }

    public void DeleteResourceAllocation(int id)
    {
        _resourceAllocationRepository.DeleteAllocation(id);
    }

    public int AddResourceAllocation(NewResourceAllocationDto resourceAllocation)
    {
        var resourceAllocationEntity = _mapper.Map<ServiceResource>(resourceAllocation);
        var allocated = _resourceAllocationRepository.GetAllocatedQuantity(resourceAllocationEntity.ResourceId,
            resourceAllocationEntity.BeginDate, resourceAllocationEntity.EndDate);
        var max = _resourceAllocationRepository.GetMaxQuantity(resourceAllocationEntity.ResourceId);
        if (allocated + resourceAllocationEntity.AllocatedQuantity > max)
            throw new Exception("Not enough resources, " + (max - allocated).ToString() + "available at the moment");
        var id = _resourceAllocationRepository.AddAllocation(resourceAllocationEntity);
        return id;
    }

    public List<ResourceAllocationDto> GetAllResourceAllocations()
    {
        var resourceAllocations = _resourceAllocationRepository.GetAllAllocations()
            .ProjectTo<ResourceAllocationDto>(_mapper.ConfigurationProvider)
            .ToList();
        return resourceAllocations;
    }

    public object GetResourceAllocation(int id)
    {
        var resourceAllocation = _resourceAllocationRepository.GetAllocation(id);
        var resourceAllocationDto = _mapper.Map<ResourceAllocationDto>(resourceAllocation);
        return resourceAllocationDto;
    }

    public object UpdateResourceAllocation(UpdateResourceAllocationDto resourceAllocation)
    {
        var resourceAllocationEntity = _mapper.Map<ServiceResource>(resourceAllocation);
        _resourceAllocationRepository.UpdateAllocation(resourceAllocationEntity);
        return resourceAllocation;
    }

    public List<ResourceAllocationDto> GetResourceAllocationsByResourceId(int resourceId)
    {
        var resourceAllocations = _resourceAllocationRepository.GetResourceAllocations(resourceId)
            .ProjectTo<ResourceAllocationDto>(_mapper.ConfigurationProvider)
            .ToList();
        return resourceAllocations;
    }

    public List<ResourceAllocationDto> GetResourceAllocationsByServiceId(int serviceId)
    {
        var resourceAllocations = _resourceAllocationRepository.GetServiceAllocations(serviceId)
            .ProjectTo<ResourceAllocationDto>(_mapper.ConfigurationProvider)
            .ToList();
        return resourceAllocations;
    }
}