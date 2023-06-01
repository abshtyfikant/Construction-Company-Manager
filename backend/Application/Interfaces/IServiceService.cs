using Application.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceService
    {
        int AddService();
        List<ServiceForListDto> GetServicesForList();
        object GetServiceForEdit(int id);
        object UpdateService();
        void DeleteService(int id);
    }
}
 