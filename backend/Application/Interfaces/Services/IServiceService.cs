using Application.DTO.Service;

namespace Application.Interfaces.Services;

public interface IServiceService
{
    int AddService(NewServiceDto album);
    List<ServiceForListDto> GetServicesForList();
    object GetService(int serviceId);
    object UpdateService(NewServiceDto newService);
    void DeleteService(int id);
}