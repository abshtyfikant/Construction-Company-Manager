using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class EmployeeSpecialization
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
