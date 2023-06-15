namespace Domain.Model;

public class Specialization
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<EmployeeSpecialization> EmployeeSpecializations { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public List<Employee> MainSpecializationEmployees { get; set; }
}