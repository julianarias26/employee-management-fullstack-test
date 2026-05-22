using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Auth.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user);
    }
}
