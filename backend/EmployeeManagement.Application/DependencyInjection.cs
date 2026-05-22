using EmployeeManagement.Application.Auth.Interfaces;
using EmployeeManagement.Application.Auth.Services;
using EmployeeManagement.Application.Employees.Interfaces;
using EmployeeManagement.Application.Employees.Services;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();

        return services;
    }
}