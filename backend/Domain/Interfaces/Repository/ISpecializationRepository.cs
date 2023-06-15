using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface ISpecializationRepository
{
    void DeleteSpecialization(int specializationId);
    int AddSpecialization(Specialization specialization);
    IQueryable<Specialization> GetAllSpecializations();
    Specialization GetSpecialization(int specializationId);
    void UpdateSpecialization(Specialization specialization);
}