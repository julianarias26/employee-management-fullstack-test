using EmployeeManagement.Application.Auth.Interfaces;
using EmployeeManagement.Application.Employees.Interfaces;
using EmployeeManagement.Domain.Services.Bonus;
using EmployeeManagement.Infrastructure.Auth;
using EmployeeManagement.Infrastructure.Persistence;
using EmployeeManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IBonusCalculator, BonusCalculator>();
        services.AddScoped<IBonusCalculationStrategy, RegularEmployeeBonusStrategy>();
        services.AddScoped<IBonusCalculationStrategy, ManagerBonusStrategy>();

        return services;
    }
}