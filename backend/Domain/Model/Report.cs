namespace Domain.Model;

public class Report
{
    public int Id { get; set; }
    public int? ServiceId { get; set; }
    public Service? Service { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public string ReportType { get; set; }
    public string Description { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Amount { get; set; }
    public string City { get; set; }
}