using EmployeeManagement.Application.Employees.Dtos;
using EmployeeManagement.Application.Employees.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.Services.Bonus;

namespace EmployeeManagement.Application.Employees.Services;

public sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBonusCalculator _bonusCalculator;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IBonusCalculator bonusCalculator)
    {
        _employeeRepository = employeeRepository;
        _bonusCalculator = bonusCalculator;
    }

    public async Task<IReadOnlyCollection<EmployeeResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var employees = await _employeeRepository.GetAllAsync(cancellationToken);

        return employees
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<EmployeeResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(
            id,
            asNoTracking: true,
            cancellationToken);

        return employee is null ? null : MapToResponse(employee);
    }

    public async Task<IReadOnlyCollection<EmployeeResponseDto>> GetByDepartmentWithProjectsAsync(
        int departmentId,
        CancellationToken cancellationToken = default)
    {
        var employees = await _employeeRepository.GetByDepartmentWithProjectsAsync(
            departmentId,
            cancellationToken);

        return employees
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<EmployeeResponseDto> CreateAsync(
        CreateEmployeeRequest request,
        CancellationToken cancellationToken = default)
    {
        var departmentExists = await _employeeRepository.DepartmentExistsAsync(
            request.DepartmentId,
            cancellationToken);

        if (!departmentExists)
            throw new ArgumentException("The selected department does not exist.");

        var employee = new Employee(
            request.Name,
            request.CurrentPosition,
            request.Salary,
            request.DepartmentId);

        await _employeeRepository.AddAsync(employee, cancellationToken);
        await _employeeRepository.SaveChangesAsync(cancellationToken);

        var createdEmployee = await _employeeRepository.GetByIdAsync(
            employee.Id,
            asNoTracking: true,
            cancellationToken);

        return MapToResponse(createdEmployee!);
    }

    public async Task<EmployeeResponseDto?> UpdateAsync(
        int id,
        UpdateEmployeeRequest request,
        CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(
            id,
            asNoTracking: false,
            cancellationToken);

        if (employee is null)
            return null;

        var departmentExists = await _employeeRepository.DepartmentExistsAsync(
            request.DepartmentId,
            cancellationToken);

        if (!departmentExists)
            throw new ArgumentException("The selected department does not exist.");

        employee.Update(
            request.Name,
            request.CurrentPosition,
            request.Salary,
            request.DepartmentId);

        await _employeeRepository.SaveChangesAsync(cancellationToken);

        var updatedEmployee = await _employeeRepository.GetByIdAsync(
            id,
            asNoTracking: true,
            cancellationToken);

        return MapToResponse(updatedEmployee!);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(
            id,
            asNoTracking: false,
            cancellationToken);

        if (employee is null)
            return false;

        _employeeRepository.Delete(employee);
        await _employeeRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    private EmployeeResponseDto MapToResponse(Employee employee)
    {
        var currentPositionName = Enum.IsDefined(typeof(PositionType), employee.CurrentPosition)
            ? ((PositionType)employee.CurrentPosition).ToString()
            : "Unknown";

        return new EmployeeResponseDto
        {
            Id = employee.Id,
            Name = employee.Name,
            CurrentPosition = employee.CurrentPosition,
            CurrentPositionName = currentPositionName,
            Salary = employee.Salary,
            AnnualBonus = employee.CalculateAnnualBonus(_bonusCalculator),
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name ?? string.Empty,
            Projects = employee.Projects
                .Select(project => project.Name)
                .OrderBy(projectName => projectName)
                .ToList(),
            PositionHistories = employee.PositionHistories
                .OrderByDescending(positionHistory => positionHistory.StartDate)
                .Select(positionHistory => new PositionHistoryResponseDto
                {
                    Id = positionHistory.Id,
                    Position = positionHistory.Position,
                    StartDate = positionHistory.StartDate,
                    EndDate = positionHistory.EndDate
                })
                .ToList()
        };
    }
}