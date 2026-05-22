using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Services.Bonus
{
    public class RegularEmployeeBonusStrategy : IBonusCalculationStrategy
    {
        private const decimal BonusPercentage = 0.10m;

        public bool CanHandle(Employee employee)
        {
            var currentPosition = (PositionType)employee.CurrentPosition;

            return !currentPosition.IsManager();
        }

        public decimal Calculate(Employee employee)
        {
            return employee.Salary * BonusPercentage;
        }
    }
}
