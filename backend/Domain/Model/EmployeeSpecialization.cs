namespace Domain.Model;

public class EmployeeSpecialization
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int SpecializationId { get; set; }
    public Specialization Specialization { get; set; }
}