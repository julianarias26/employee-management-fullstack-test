using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Services.Bonus
{
    public class BonusCalculator : IBonusCalculator
    {
        private readonly IEnumerable<IBonusCalculationStrategy> _strategies;

        public BonusCalculator(IEnumerable<IBonusCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public decimal Calculate(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            var strategy = _strategies.FirstOrDefault(strategy => strategy.CanHandle(employee));

            if (strategy is null)
                throw new InvalidOperationException("No bonus calculation strategy was found for the employee.");

            return strategy.Calculate(employee);
        }
    }
}
