using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Services.Bonus
{
    public interface IBonusCalculator
    {
        decimal Calculate(Employee employee);
    }
}
