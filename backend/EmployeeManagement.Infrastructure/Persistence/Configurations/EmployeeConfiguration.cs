using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(employee => employee.Id);

            builder.Property(employee => employee.Id)
                .ValueGeneratedOnAdd();

            builder.Property(employee => employee.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(employee => employee.CurrentPosition)
                .IsRequired();

            builder.Property(employee => employee.Salary)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(employee => employee.DepartmentId)
                .IsRequired();

            builder.HasOne(employee => employee.Department)
                .WithMany(department => department.Employees)
                .HasForeignKey(employee => employee.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(employee => employee.PositionHistories)
                .WithOne(positionHistory => positionHistory.Employee)
                .HasForeignKey(positionHistory => positionHistory.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(employee => employee.DepartmentId);

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = "John Smith",
                    CurrentPosition = 1,
                    Salary = 3500m,
                    DepartmentId = 1
                },
                new
                {
                    Id = 2,
                    Name = "Sarah Johnson",
                    CurrentPosition = 2,
                    Salary = 5500m,
                    DepartmentId = 1
                },
                new
                {
                    Id = 3,
                    Name = "Michael Brown",
                    CurrentPosition = 1,
                    Salary = 3200m,
                    DepartmentId = 2
                }
            );
        }
    }
}
