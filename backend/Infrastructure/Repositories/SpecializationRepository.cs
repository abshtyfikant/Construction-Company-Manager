using Domain.Interfaces.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class SpecializationRepository : ISpecializationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecializationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddSpecialization(Specialization specialization)
        {
            _dbContext.Specializations.Add(specialization);
            _dbContext.SaveChanges();
            return specialization.Id;
        }

        public void DeleteSpecialization(int specializationId)
        {
            var specialization = _dbContext.Specializations.Find(specializationId);
            if (specialization is not null)
            {
                _dbContext.Specializations.Remove(specialization);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Specialization> GetAllSpecializations()
        {
            var specializations = _dbContext.Specializations;
            return specializations;
        }

        public Specialization GetSpecialization(int specializationId)
        {
            var specialization = _dbContext.Specializations.FirstOrDefault(i => i.Id == specializationId);
            return specialization;
        }

        public void UpdateSpecialization(Specialization specialization)
        {
            _dbContext.Attach(specialization);
            _dbContext.Entry(specialization).Property("Name").IsModified = true;
            _dbContext.SaveChanges();
        }
    }
}
