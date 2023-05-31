using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public List<ServiceResource> ServiceResources { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
