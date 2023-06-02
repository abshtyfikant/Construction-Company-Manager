using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IServiceRepository
    {
        void DeleteService(int serviceId);
        int AddService(Service service);
        IQueryable<Service> GetAllServices();
        Service GetService(int serviceId);
        void UpdateService(Service service);
    }
}
