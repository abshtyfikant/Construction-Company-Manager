using Application.DTO.Specialization;

namespace Application.Interfaces.Services;

public interface ISpecializationService
{
    int AddSpecialization(NewSpecializationDto specialization);
    List<SpecializationDto> GetSpecializationsForList();
    object GetSpecialization(int specializationId);
    object UpdateSpecialization(NewSpecializationDto newSpecialization);
    void DeleteSpecialization(int specializationId);
}