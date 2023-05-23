using ConstructionCompanyManager.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionCompanyManager.Infrastructure
{
    public class Context : IdentityDbContext
    {

        public DbSet<Assigment> Assigments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSpecialization> SpecializationAssigments { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceResource> ResourceAllocations { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
                .UsingEntity<Assigment>();


            builder.Entity<Employee>()
            .HasMany(p => p.Services)
            .WithMany(p => p.Employees)
            .UsingEntity<Assigment>(
                j => j
                    .HasOne(pt => pt.Service)
                    .WithMany(t => t.Assigments)
                    .HasForeignKey(pt => pt.ServiceId),
                j => j
                    .HasOne(pt => pt.Employee)
                    .WithMany(p => p.Assigments)
                    .HasForeignKey(pt => pt.EmployeeId),
                j =>
                { 
                    j.HasKey(t => new { t.EmployeeId, t.ServiceId });
                });

            builder.Entity<Employee>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .IsRequired();

            builder.Entity<Service>()
                .HasOne(e => e.Client)
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.ClientId)
                .IsRequired();

            builder.Entity<Service>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceID)
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
                .IsRequired();

            builder.Entity<Service>()
            .HasMany(p => p.Resources)
            .WithMany(p => p.Services)
            .UsingEntity<ServiceResource>(
                j => j
                    .HasOne(pt => pt.Resource)
                    .WithMany(t => t.ServiceResources)
                    .HasForeignKey(pt => pt.ResourceId),
                j => j
                    .HasOne(pt => pt.Service)
                    .WithMany(p => p.ServiceResources)
                    .HasForeignKey(pt => pt.ServiceId),
                j =>
                {
                    j.HasKey(t => new { t.ResourceId, t.ServiceId });
                });

        }
    }
}
