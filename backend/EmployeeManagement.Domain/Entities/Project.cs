namespace EmployeeManagement.Domain.Entities
{
    public class Project
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        private Project()
        {
        }

        public Project(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required.", nameof(name));

            Name = name.Trim();
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required.", nameof(name));

            Name = name.Trim();
        }
    }
}
