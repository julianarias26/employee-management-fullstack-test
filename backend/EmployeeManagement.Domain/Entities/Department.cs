namespace EmployeeManagement.Domain.Entities
{
    public class Department
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        private Department()
        {
        }

        public Department(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Department name is required.", nameof(name));

            Name = name.Trim();
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Department name is required.", nameof(name));

            Name = name.Trim();
        }
    }
}
