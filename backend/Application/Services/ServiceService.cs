using Application.DTO.Service;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly IMapper _mapper;

        public ServiceService(IMapper mapper, IServiceRepository serviceRepo)
        {
            _mapper = mapper;
            _serviceRepo = serviceRepo;
        }

        public int AddService(NewServiceDto service)
        {
            var ser = _mapper.Map<Service>(service);
            var id = _serviceRepo.AddService(ser);
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

        public object GetServiceForEdit(int id)
        {
            throw new NotImplementedException();
        }

        public List<ServiceForListDto> GetServicesForList()
        {
            var services = _serviceRepo.GetAllServices()
            .ProjectTo<ServiceForListDto>(_mapper.ConfigurationProvider)
            .ToList();
            return services;
;
        }

        public object UpdateService(NewServiceDto newService)
        {
            var service = _mapper.Map<Service>(newService);
            _serviceRepo.UpdateService(service);
            return service;
        }
    }
}
