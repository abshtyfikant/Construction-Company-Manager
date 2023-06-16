namespace Domain.Model;

public class Assigment
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public string Function { get; set; }
}