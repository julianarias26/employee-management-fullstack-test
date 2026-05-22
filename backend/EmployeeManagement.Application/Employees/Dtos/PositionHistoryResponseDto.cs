namespace EmployeeManagement.Application.Employees.Dtos
{
    public sealed class PositionHistoryResponseDto
    {
        public int Id { get; set; }

        public string Position { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
