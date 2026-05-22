using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.Services.Bonus;

namespace EmployeeManagement.Domain.Entities
{
    public class Employee
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public int CurrentPosition { get; private set; }

        public decimal Salary { get; private set; }

        public int DepartmentId { get; private set; }

        public Department? Department { get; private set; }

        public ICollection<PositionHistory> PositionHistories { get; private set; } = new List<PositionHistory>();

        public ICollection<Project> Projects { get; private set; } = new List<Project>();

        private Employee()
        {
        }

        public Employee(
            string name,
            int currentPosition,
            decimal salary,
            int departmentId)
        {
            Update(name, currentPosition, salary, departmentId);
        }

        public void Update(
            string name,
            int currentPosition,
            decimal salary,
            int departmentId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Employee name is required.", nameof(name));

            if (!Enum.IsDefined(typeof(PositionType), currentPosition))
                throw new ArgumentException("Current position is not valid.", nameof(currentPosition));

            if (salary <= 0)
                throw new ArgumentException("Salary must be greater than zero.", nameof(salary));

            if (departmentId <= 0)
                throw new ArgumentException("Department id must be greater than zero.", nameof(departmentId));

            Name = name.Trim();
            CurrentPosition = currentPosition;
            Salary = salary;
            DepartmentId = departmentId;
        }

        public decimal CalculateAnnualBonus(IBonusCalculator bonusCalculator)
        {
            if (bonusCalculator is null)
                throw new ArgumentNullException(nameof(bonusCalculator));

            return bonusCalculator.Calculate(this);
        }

        public void AddPositionHistory(PositionHistory positionHistory)
        {
            if (positionHistory is null)
                throw new ArgumentNullException(nameof(positionHistory));

            PositionHistories.Add(positionHistory);
        }
    }
}
