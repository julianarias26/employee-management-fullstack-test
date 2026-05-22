using EmployeeManagement.Application.Auth.Dtos;
using EmployeeManagement.Application.Auth.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Application.Auth.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IAppUserRepository userRepository,
            IPasswordHasher<AppUser> passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var emailExists = await _userRepository.EmailExistsAsync(
                normalizedEmail,
                cancellationToken);

            if (emailExists)
                throw new ArgumentException("Email is already registered.");

            if (!Enum.TryParse<UserRole>(request.Role, ignoreCase: true, out var parsedRole))
                throw new ArgumentException("Role is not valid. Allowed values are Admin or User.");

            var temporaryUser = new AppUser(
                request.FullName,
                normalizedEmail,
                "TEMPORARY_PASSWORD_HASH",
                parsedRole.ToString());

            var passwordHash = _passwordHasher.HashPassword(
                temporaryUser,
                request.Password);

            var user = new AppUser(
                request.FullName,
                normalizedEmail,
                passwordHash,
                parsedRole.ToString());

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var user = await _userRepository.GetByEmailAsync(
                normalizedEmail,
                cancellationToken);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }
    }
}
