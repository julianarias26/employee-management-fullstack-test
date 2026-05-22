using EmployeeManagement.Application.Auth.Dtos;

namespace EmployeeManagement.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default);

        Task<AuthResponseDto> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default);
    }
}
