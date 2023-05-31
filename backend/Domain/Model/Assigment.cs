using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Assigment
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ServiceId { get; set; }
        public Service Service{ get; set; }
        public string Function { get; set; }
    }
}
