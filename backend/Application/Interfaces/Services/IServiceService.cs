using Application.DTO.Assigment;
using Application.DTO.Material;
using Application.DTO.ResourceAllocation;
using Application.DTO.Service;

namespace Application.Interfaces.Services;

public interface IServiceService
{
    int AddService(NewServiceDto service, List<NewAssignmentDto> assignments, List<NewResourceAllocationDto> resources, List<NewMaterialDto> materials);
    List<ServiceForListDto> GetServicesForList();
    object GetService(int serviceId);
    object UpdateService(NewServiceDto service, List<NewAssignmentDto> assignments, List<NewResourceAllocationDto> resources, List<NewMaterialDto> materials);
    void DeleteService(int id);
    double GetServiceEarnings(DateTime startDate, DateTime endDate);
    double GetServiceCost(int serviceId);
}
