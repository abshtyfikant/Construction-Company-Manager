using Application.DTO.Resource;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;

    public ResourceService(IMapper mapper, IResourceRepository resourceRepository)
    {
        _mapper = mapper;
        _resourceRepository = resourceRepository;
    }

    public int AddResource(NewResourceDto resource)
    {
        var resourceEntity = _mapper.Map<Resource>(resource);
        var id = _resourceRepository.AddResource(resourceEntity);
        return id;
    }

    public List<ResourceDto> GetResourcesForList()
    {
        var resources = _resourceRepository.GetAll()
            .ProjectTo<ResourceDto>(_mapper.ConfigurationProvider)
            .ToList();
        return resources;
    }

    public object GetResource(int resourceId)
    {
        var resource = _resourceRepository.GetResource(resourceId);
        var resourceDto = _mapper.Map<ResourceDto>(resource);
        return resourceDto;
    }

    public object UpdateResource(NewResourceDto newResource)
    {
        var resourceEntity = _mapper.Map<Resource>(newResource);
        _resourceRepository.UpdateResource(resourceEntity);
        return resourceEntity;
    }

    public void DeleteResource(int resourceId)
    {
        _resourceRepository.DeleteResource(resourceId);
    }

    public void ChangeQuantity(int resourceId, int quantity)
    {
        _resourceRepository.ChangeQuantity(resourceId, quantity);
    }

    public double GetAvailableQuantityForTime(int id, DateTime startTime, DateTime endTime)
    {
        var quantity = _resourceRepository.GetAvailableQuantityForTime(id, startTime, endTime);
        return quantity;
    }
}