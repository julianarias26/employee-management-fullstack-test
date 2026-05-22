using EmployeeManagement.Application.Employees.Interfaces;
using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Employee>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(employee => employee.Department)
            .Include(employee => employee.Projects)
            .Include(employee => employee.PositionHistories)
            .OrderBy(employee => employee.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Employee?> GetByIdAsync(
        int id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Employees
            .Include(employee => employee.Department)
            .Include(employee => employee.Projects)
            .Include(employee => employee.PositionHistories)
            .AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(
            employee => employee.Id == id,
            cancellationToken);
    }

    public async Task<IReadOnlyCollection<Employee>> GetByDepartmentWithProjectsAsync(
        int departmentId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(employee => employee.Department)
            .Include(employee => employee.Projects)
            .Include(employee => employee.PositionHistories)
            .Where(employee => employee.DepartmentId == departmentId)
            .Where(employee => employee.Projects.Any())
            .OrderBy(employee => employee.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> DepartmentExistsAsync(
        int departmentId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .AnyAsync(department => department.Id == departmentId, cancellationToken);
    }

    public async Task AddAsync(
        Employee employee,
        CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
    }

    public void Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}