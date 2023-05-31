using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IServiceRepository
    {
        void DeleteService(int serviceId);
        int AddService(Service service);
        IQueryable<Service> GetAllServices();
        Service GetServiceById(int serviceId);
        void UpdateService(Service service);
    }
}
