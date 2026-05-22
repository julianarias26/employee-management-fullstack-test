import type { EmployeeResponse } from "../../models/employee";

interface EmployeeTableProps {
  employees: EmployeeResponse[];
  canManageEmployees: boolean;
  onEdit: (employee: EmployeeResponse) => void;
  onDelete: (id: number) => Promise<void>;
}

function formatCurrency(value: number): string {
  return new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
  }).format(value);
}

function EmployeeTable({
  employees,
  canManageEmployees,
  onEdit,
  onDelete,
}: EmployeeTableProps) {
  if (employees.length === 0) {
    return (
      <section className="card">
        <p>No se encontraron empleados.</p>
      </section>
    );
  }

  return (
    <section className="card">
      <h2>Empleados</h2>

      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Cargo</th>
              <th>Departmento</th>
              <th>Salario</th>
              <th>Bono anual</th>
              <th>Proyectos</th>
              {canManageEmployees && <th>Acciones</th>}
            </tr>
          </thead>

          <tbody>
            {employees.map((employee) => (
              <tr key={employee.id}>
                <td>{employee.name}</td>
                <td>{employee.currentPositionName}</td>
                <td>{employee.departmentName}</td>
                <td>{formatCurrency(employee.salary)}</td>
                <td>{formatCurrency(employee.annualBonus)}</td>
                <td>
                  {employee.projects.length > 0
                    ? employee.projects.join(", ")
                    : "Sin proyectos"}
                </td>

                {canManageEmployees && (
                  <td>
                    <div className="table-actions">
                      <button
                        type="button"
                        className="small-button"
                        onClick={() => onEdit(employee)}
                      >
                        Editar
                      </button>

                      <button
                        type="button"
                        className="danger-button small-button"
                        onClick={() => onDelete(employee.id)}
                      >
                        Eliminar
                      </button>
                    </div>
                  </td>
                )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  );
}

export default EmployeeTable;