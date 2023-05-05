using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionCompanyManager.Domain.Model
{
    public class Service
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string ServiceType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ServiceStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string City { get; set; }
        public List<ServiceResource> ServiceResources { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Material> Materials { get; set; }
        public List<Report> Reports { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public List<Assigment> Assigments { get; set; }
    }
}
