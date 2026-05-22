using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.Persistence.Configurations
{
    public class PositionHistoryConfiguration : IEntityTypeConfiguration<PositionHistory>
    {
        public void Configure(EntityTypeBuilder<PositionHistory> builder)
        {
            builder.ToTable("PositionHistory");

            builder.HasKey(positionHistory => positionHistory.Id);

            builder.Property(positionHistory => positionHistory.Id)
                .ValueGeneratedOnAdd();

            builder.Property(positionHistory => positionHistory.EmployeeId)
                .IsRequired();

            builder.Property(positionHistory => positionHistory.Position)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(positionHistory => positionHistory.StartDate)
                .IsRequired();

            builder.Property(positionHistory => positionHistory.EndDate)
                .IsRequired(false);

            builder.HasOne(positionHistory => positionHistory.Employee)
                .WithMany(employee => employee.PositionHistories)
                .HasForeignKey(positionHistory => positionHistory.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(positionHistory => positionHistory.EmployeeId);

            builder.HasData(
                new
                {
                    Id = 1,
                    EmployeeId = 1,
                    Position = "Software Developer",
                    StartDate = new DateTime(2022, 1, 1),
                    EndDate = (DateTime?)null
                },
                new
                {
                    Id = 2,
                    EmployeeId = 2,
                    Position = "Project Manager",
                    StartDate = new DateTime(2021, 6, 1),
                    EndDate = (DateTime?)null
                }
            );
        }
    }
}
