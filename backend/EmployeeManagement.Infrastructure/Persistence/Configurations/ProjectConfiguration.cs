using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(project => project.Id);

            builder.Property(project => project.Id)
                .ValueGeneratedOnAdd();

            builder.Property(project => project.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(project => project.Name)
                .IsUnique();

            builder.HasMany(project => project.Employees)
                .WithMany(employee => employee.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeProjects",
                    right => right
                        .HasOne<Employee>()
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade),
                    left => left
                        .HasOne<Project>()
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.ToTable("EmployeeProjects");

                        join.HasKey("EmployeeId", "ProjectId");

                        join.HasData(
                            new { EmployeeId = 1, ProjectId = 1 },
                            new { EmployeeId = 2, ProjectId = 1 },
                            new { EmployeeId = 2, ProjectId = 2 }
                        );
                    });

            builder.HasData(
                new { Id = 1, Name = "Internal Management System" },
                new { Id = 2, Name = "Payroll Modernization" },
                new { Id = 3, Name = "Reporting Dashboard" }
            );
        }
    }
}
