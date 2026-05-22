using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Auth.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<bool> EmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            AppUser user,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
