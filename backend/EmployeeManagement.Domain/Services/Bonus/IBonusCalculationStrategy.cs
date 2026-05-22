using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Services.Bonus
{
    public interface IBonusCalculationStrategy
    {
        bool CanHandle(Employee employee);

        decimal Calculate(Employee employee);
    }
}
