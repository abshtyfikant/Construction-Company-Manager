using Application.DTO.Specialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISpecializationService
    {
        int AddSpecialization(NewSpecializationDto specialization);
        List<SpecializationDto> GetSpecializationsForList();
        object GetSpecialization(int specializationId);
        object UpdateSpecialization(NewSpecializationDto newSpecialization);
        void DeleteSpecialization(int specializationId);
    }
}
