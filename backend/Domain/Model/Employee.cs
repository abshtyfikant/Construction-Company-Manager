namespace Domain.Model;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public double RatePerHour { get; set; }
    public int MainSpecializationId { get; set; }
    public Specialization MainSpecialization { get; set; }
    public List<EmployeeSpecialization> EmployeeSpecializations { get; set; }
    public List<Specialization> Specializations { get; set; }
    public List<Assignment> Assigments { get; set; }
    public List<Report> Reports { get; set; }
    public ICollection<Service> Services { get; set; }
}