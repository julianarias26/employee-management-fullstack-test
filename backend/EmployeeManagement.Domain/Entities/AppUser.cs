using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; private set; }

        public string FullName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public string Role { get; private set; } = string.Empty;

        private AppUser()
        {
        }

        public AppUser(
            string fullName,
            string email,
            string passwordHash,
            string role)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.", nameof(fullName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash is required.", nameof(passwordHash));

            if (!Enum.TryParse<UserRole>(role, ignoreCase: true, out var parsedRole))
                throw new ArgumentException("Role is not valid.", nameof(role));

            FullName = fullName.Trim();
            Email = email.Trim().ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = parsedRole.ToString();
        }
    }
}
