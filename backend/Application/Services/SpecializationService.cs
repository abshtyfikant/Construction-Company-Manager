using Application.DTO.Specialization;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepo;
        private readonly IMapper _mapper;

        public SpecializationService(ISpecializationRepository specializationRepo, IMapper mapper)
        {
            _specializationRepo = specializationRepo;
            _mapper = mapper;
        }


        public int AddSpecialization(NewSpecializationDto specialization)
        {
            var spec = _mapper.Map<Specialization>(specialization);
            var id = _specializationRepo.AddSpecialization(spec);
            return id;
        }

        public void DeleteSpecialization(int specializationId)
        {
            _specializationRepo.DeleteSpecialization(specializationId);
        }

        public object GetSpecialization(int specializationId)
        {
            var specialization = _specializationRepo.GetSpecialization(specializationId);
            var specializationDto = _mapper.Map<SpecializationDto>(specialization);
            return specializationDto;
        }

        public List<SpecializationDto> GetSpecializationsForList()
        {
            var specializations = _specializationRepo.GetAllSpecializations()
                .ProjectTo<SpecializationDto>(_mapper.ConfigurationProvider)
                .ToList();
            return specializations;
        }

        public object UpdateSpecialization(NewSpecializationDto newSpecialization)
        {
            var specialization = _mapper.Map<Specialization>(newSpecialization);
            _specializationRepo.UpdateSpecialization(specialization);
            return specialization;
        }
    }
}
