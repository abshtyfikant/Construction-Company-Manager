using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ServiceResource
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
        public double AllocatedQuantity { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
