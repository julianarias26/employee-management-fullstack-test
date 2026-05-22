namespace EmployeeManagement.Domain.Enums
{
    public static class PositionTypeExtensions
    {
        public static bool IsManager(this PositionType positionType)
        {
            return positionType is PositionType.Manager
                or PositionType.SeniorManager
                or PositionType.ProjectManager
                or PositionType.DepartmentManager;
        }
    }
}
