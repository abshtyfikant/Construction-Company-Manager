using Application.DTO.Assigment;
using Application.DTO.Material;
using Application.DTO.ResourceAllocation;
using Application.DTO.Service;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

internal class ServiceService : IServiceService
{
    private readonly IMapper _mapper;
    private readonly IServiceRepository _serviceRepo;

    public ServiceService(IMapper mapper, IServiceRepository serviceRepo)
    {
        _mapper = mapper;
        _serviceRepo = serviceRepo;
    }

    public int AddService(NewServiceDto service, List<NewAssignmentDto> assignments, List<NewResourceAllocationDto> resources, List<NewMaterialDto> materials)
    {
        var mappedService = _mapper.Map<Service>(service);

        var mappedAssignments = new List<Assignment> { };
        foreach(var assignment in assignments)
        {
            mappedAssignments.Add(_mapper.Map<Assignment>(assignment));
        }

        var mappedResources = new List<ServiceResource> { };
        foreach (var resource in resources)
        {
            mappedResources.Add(_mapper.Map<ServiceResource>(resource));
        }

        var mappedMaterials = new List<Material> { };
        foreach (var material in materials)
        {
            mappedMaterials.Add(_mapper.Map<Material>(material));
        }

        var id = _serviceRepo.AddService(mappedService, mappedAssignments, mappedResources, mappedMaterials);
        return id;
    }

    public void DeleteService(int id)
    {
        _serviceRepo.DeleteService(id);
    }

    public object GetService(int serviceId)
    {
        var service = _serviceRepo.GetService(serviceId);
        var serviceDto = _mapper.Map<ServiceDetailsDto>(service);
        return serviceDto;
    }

    public List<ServiceForListDto> GetServicesForList()
    {
        var services = _serviceRepo.GetAllServices()
            .ProjectTo<ServiceForListDto>(_mapper.ConfigurationProvider)
            .ToList();
        return services;
        ;
    }

    public object UpdateService(NewServiceDto service, List<NewAssignmentDto> assignments, List<NewResourceAllocationDto> resources, List<NewMaterialDto> materials)
    {
        var mappedService = _mapper.Map<Service>(service);

        var mappedAssignments = new List<Assignment> { };
        foreach (var assignment in assignments)
        {
            mappedAssignments.Add(_mapper.Map<Assignment>(assignment));
        }

        var mappedResources = new List<ServiceResource> { };
        foreach (var resource in resources)
        {
            mappedResources.Add(_mapper.Map<ServiceResource>(resource));
        }

        var mappedMaterials = new List<Material> { };
        foreach (var material in materials)
        {
            mappedMaterials.Add(_mapper.Map<Material>(material));
        }

        _serviceRepo.UpdateService(mappedService, mappedAssignments, mappedResources, mappedMaterials);
        return service;
    }
}