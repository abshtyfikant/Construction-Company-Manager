namespace Domain.Model;

public class Material
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }
}