namespace Domain.Model;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Quantity { get; set; }
    public List<ServiceResource> ServiceResources { get; set; }
    public ICollection<Service> Services { get; set; }
}