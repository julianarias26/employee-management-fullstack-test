using EmployeeManagement.Application.Employees.Dtos;

namespace EmployeeManagement.Application.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<IReadOnlyCollection<EmployeeResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<EmployeeResponseDto>> GetByDepartmentWithProjectsAsync(
            int departmentId,
            CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto> CreateAsync(
            CreateEmployeeRequest request,
            CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto?> UpdateAsync(
            int id,
            UpdateEmployeeRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
