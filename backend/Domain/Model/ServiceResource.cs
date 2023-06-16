namespace Domain.Model;

public class ServiceResource
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public int ResourceId { get; set; }
    public Resource Resource { get; set; }
    public double AllocatedQuantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}