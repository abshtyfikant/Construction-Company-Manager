using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface ISpecializationRepository
    {
        void DeleteSpecialization(int specializationId);
        int AddSpecialization(Specialization specialization);
        IQueryable<Specialization> GetAllSpecializations();
        Specialization GetSpecialization(int specializationId);
        void UpdateSpecialization(Specialization specialization);
    }
}
