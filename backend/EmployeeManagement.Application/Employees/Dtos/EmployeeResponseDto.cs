namespace EmployeeManagement.Application.Employees.Dtos
{
    public sealed class EmployeeResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CurrentPosition { get; set; }

        public string CurrentPositionName { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public decimal AnnualBonus { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public IReadOnlyCollection<string> Projects { get; set; } = [];

        public IReadOnlyCollection<PositionHistoryResponseDto> PositionHistories { get; set; } = [];
    }
}
