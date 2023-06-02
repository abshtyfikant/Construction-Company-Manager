using Application.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IServiceService
    {
        int AddService(NewServiceDto album);
        List<ServiceForListDto> GetServicesForList();
        object GetService(int serviceId);
        object GetServiceForEdit(int id);
        object UpdateService();
        void DeleteService(int id);
    }
}
