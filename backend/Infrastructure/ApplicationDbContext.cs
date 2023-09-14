using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeSpecialization> SpecializationAssignments { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceResource> ResourceAllocations { get; set; }
    public DbSet<Specialization> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Report>()
            .HasOne(e => e.User)
            .WithMany(e => e.Reports)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.Entity<Report>()
            .HasOne(e => e.Employee)
            .WithMany(e => e.Reports)
            .HasForeignKey(e => e.EmployeeId)
            .IsRequired(false);

        builder.Entity<Employee>()
            .HasMany(e => e.Specializations)
            .WithMany(e => e.Employees)
            .UsingEntity<EmployeeSpecialization>();

        builder.Entity<Employee>()
            .HasOne(e => e.MainSpecialization)
            .WithMany(e => e.MainSpecializationEmployees)
            .HasForeignKey(e => e.MainSpecializationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Employee>()
            .HasMany(e => e.Services)
            .WithMany(e => e.Employees)
            .UsingEntity<Assignment>();

        builder.Entity<Employee>()  
            .HasMany(e => e.Assigments)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeId)
            .IsRequired();

        builder.Entity<Service>()
            .HasMany(p => p.Assigments)
            .WithOne(p => p.Service)
            .HasForeignKey(e => e.ServiceId)
            .IsRequired();

        builder.Entity<User>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.Entity<Service>()
            .HasOne(e => e.Client)
            .WithMany(e => e.Services)
            .HasForeignKey(e => e.ClientId)
            .IsRequired();

        builder.Entity<Service>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId)
            .IsRequired();

        builder.Entity<Service>()
            .HasMany(e => e.Materials)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId)
            .IsRequired();

        builder.Entity<Service>()
            .HasMany(e => e.Reports)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId)
            .IsRequired(false);

        builder.Entity<Service>()
            .HasMany(e => e.ServiceResources)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId)
            .IsRequired();

        builder.Entity<Resource>()
            .HasMany(e => e.ServiceResources)
            .WithOne(e => e.Resource)
            .HasForeignKey(e => e.ResourceId)
            .IsRequired();
    }
}