namespace EmployeeManagement.Domain.Entities
{
    public class PositionHistory
    {
        public int Id { get; private set; }

        public int EmployeeId { get; private set; }

        public string Position { get; private set; } = string.Empty;

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public Employee? Employee { get; private set; }

        private PositionHistory()
        {
        }

        public PositionHistory(
            int employeeId,
            string position,
            DateTime startDate,
            DateTime? endDate = null)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Employee id must be greater than zero.", nameof(employeeId));

            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("Position is required.", nameof(position));

            if (endDate.HasValue && endDate.Value.Date < startDate.Date)
                throw new ArgumentException("End date cannot be earlier than start date.", nameof(endDate));

            EmployeeId = employeeId;
            Position = position.Trim();
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Close(DateTime endDate)
        {
            if (endDate.Date < StartDate.Date)
                throw new ArgumentException("End date cannot be earlier than start date.", nameof(endDate));

            EndDate = endDate;
        }
    }
}
