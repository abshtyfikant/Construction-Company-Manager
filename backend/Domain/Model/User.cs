namespace Domain.Model;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public List<Report> Reports { get; set; }
    public List<Comment> Comments { get; set; }
}