using EmployeeManagement.Application.Auth.Interfaces;
using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Persistence.Repositories
{
    public sealed class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();

            return await _context.AppUsers
                .FirstOrDefaultAsync(user => user.Email == normalizedEmail, cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();

            return await _context.AppUsers
                .AsNoTracking()
                .AnyAsync(user => user.Email == normalizedEmail, cancellationToken);
        }

        public async Task AddAsync(
            AppUser user,
            CancellationToken cancellationToken = default)
        {
            await _context.AppUsers.AddAsync(user, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
