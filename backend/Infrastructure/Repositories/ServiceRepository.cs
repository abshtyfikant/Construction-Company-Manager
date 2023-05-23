using ConstructionCompanyManager.Domain.Model;
using ConstructionCompanyManager.Infrastructure;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly Context _context;
        public ServiceRepository(Context context)
        {
            _context = context;
        }

        public int AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return service.Id;
        }

        public void DeleteService(int serviceId)
        {
            var service = _context.Services.Find(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }

        public IQueryable<Service> GetAllServices()
        {
            var services = _context.Services;
            return services;
        }

        public Service GetServiceById(int serviceId)
        {
            var service = _context.Services.FirstOrDefault(i => i.Id == serviceId);
            return service;
        }

        public void UpdateService(Service service)
        {
            _context.Attach(service);
            _context.Entry(service).Property("ServiceType").IsModified = true;
            _context.Entry(service).Property("BeginDate").IsModified = true;
            _context.Entry(service).Property("EndDate").IsModified = true;
            _context.Entry(service).Property("ServiceStatus").IsModified = true;
            _context.Entry(service).Property("PaymentStatus").IsModified = true;
            _context.Entry(service).Property("City").IsModified = true;
            _context.SaveChanges();
        }
    }
}
