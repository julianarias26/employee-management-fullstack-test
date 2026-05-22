using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(department => department.Id);

            builder.Property(department => department.Id)
                .ValueGeneratedOnAdd();

            builder.Property(department => department.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(department => department.Name)
                .IsUnique();

            builder.HasMany(department => department.Employees)
                .WithOne(employee => employee.Department)
                .HasForeignKey(employee => employee.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new { Id = 1, Name = "Information Technology" },
                new { Id = 2, Name = "Human Resources" },
                new { Id = 3, Name = "Finance" },
                new { Id = 4, Name = "Operations" }
            );
        }
    }
}