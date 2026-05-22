using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Employees.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IReadOnlyCollection<Employee>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Employee?> GetByIdAsync(
            int id,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<Employee>> GetByDepartmentWithProjectsAsync(
            int departmentId,
            CancellationToken cancellationToken = default);

        Task<bool> DepartmentExistsAsync(
            int departmentId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Employee employee,
            CancellationToken cancellationToken = default);

        void Delete(Employee employee);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
