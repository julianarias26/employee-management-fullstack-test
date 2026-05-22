using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.Employees.Dtos
{
    public sealed class CreateEmployeeRequest
    {
        [Required(ErrorMessage = "Employee name is required.")]
        [MaxLength(150, ErrorMessage = "Employee name cannot exceed 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Current position must be between 1 and 5.")]
        public int CurrentPosition { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than zero.")]
        public decimal Salary { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Department id is required.")]
        public int DepartmentId { get; set; }
    }
}
