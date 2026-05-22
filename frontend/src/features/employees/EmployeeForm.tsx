import { useEffect, useState } from "react";
import type {
  CreateEmployeeRequest,
  EmployeeResponse,
  UpdateEmployeeRequest,
} from "../../models/employee";

interface EmployeeFormProps {
  selectedEmployee: EmployeeResponse | null;
  isSubmitting: boolean;
  onCreate: (request: CreateEmployeeRequest) => Promise<void>;
  onUpdate: (id: number, request: UpdateEmployeeRequest) => Promise<void>;
  onCancelEdit: () => void;
}

interface EmployeeFormState {
  name: string;
  currentPosition: number;
  salary: number;
  departmentId: number;
}

const initialFormState: EmployeeFormState = {
  name: "",
  currentPosition: 1,
  salary: 0,
  departmentId: 1,
};

const positions = [
  { value: 1, label: "Regular" },
  { value: 2, label: "Manager" },
  { value: 3, label: "Senior Manager" },
  { value: 4, label: "Project Manager" },
  { value: 5, label: "Department Manager" },
];

const departments = [
  { value: 1, label: "Information Technology" },
  { value: 2, label: "Human Resources" },
  { value: 3, label: "Finance" },
  { value: 4, label: "Operations" },
];

function EmployeeForm({
  selectedEmployee,
  isSubmitting,
  onCreate,
  onUpdate,
  onCancelEdit,
}: EmployeeFormProps) {
  const [formState, setFormState] = useState<EmployeeFormState>(initialFormState);
  const [validationError, setValidationError] = useState("");

  const isEditMode = selectedEmployee !== null;

  useEffect(() => {
    if (selectedEmployee) {
      setFormState({
        name: selectedEmployee.name,
        currentPosition: selectedEmployee.currentPosition,
        salary: selectedEmployee.salary,
        departmentId: selectedEmployee.departmentId,
      });

      setValidationError("");
      return;
    }

    setFormState(initialFormState);
  }, [selectedEmployee]);

  function validateForm(): boolean {
    if (!formState.name.trim()) {
      setValidationError("El nombre del empleado es requerido.");
      return false;
    }

    if (formState.salary <= 0) {
      setValidationError("El salario debe ser mayor que cero.");
      return false;
    }

    if (formState.currentPosition < 1 || formState.currentPosition > 5) {
      setValidationError("La posición actual no es válida.");
      return false;
    }

    if (formState.departmentId <= 0) {
      setValidationError("El departamento es requerido.");
      return false;
    }

    setValidationError("");
    return true;
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const isValid = validateForm();

    if (!isValid) {
      return;
    }

    const request = {
      name: formState.name.trim(),
      currentPosition: formState.currentPosition,
      salary: formState.salary,
      departmentId: formState.departmentId,
    };

    if (isEditMode) {
      await onUpdate(selectedEmployee.id, request);
      return;
    }

    await onCreate(request);
    setFormState(initialFormState);
  }

  function handleNameChange(event: React.ChangeEvent<HTMLInputElement>) {
    setFormState({
      ...formState,
      name: event.target.value,
    });
  }

  function handlePositionChange(event: React.ChangeEvent<HTMLSelectElement>) {
    setFormState({
      ...formState,
      currentPosition: Number(event.target.value),
    });
  }

  function handleSalaryChange(event: React.ChangeEvent<HTMLInputElement>) {
    setFormState({
      ...formState,
      salary: Number(event.target.value),
    });
  }

  function handleDepartmentChange(event: React.ChangeEvent<HTMLSelectElement>) {
    setFormState({
      ...formState,
      departmentId: Number(event.target.value),
    });
  }

  function handleCancel() {
    setValidationError("");
    setFormState(initialFormState);
    onCancelEdit();
  }

  return (
    <section className="card">
      <h2>{isEditMode ? "Editar empleado" : "Crear empleado"}</h2>

      {validationError && <p className="field-error">{validationError}</p>}

      <form className="form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="employee-name">Nombre</label>
          <input
            id="employee-name"
            type="text"
            value={formState.name}
            onChange={handleNameChange}
            placeholder="Nombre del empleado"
          />
        </div>

        <div className="form-grid">
          <div className="form-group">
            <label htmlFor="employee-position">Cargo actual</label>
            <select
              id="employee-position"
              value={formState.currentPosition}
              onChange={handlePositionChange}
            >
              {positions.map((position) => (
                <option key={position.value} value={position.value}>
                  {position.label}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="employee-department">Departamento</label>
            <select
              id="employee-department"
              value={formState.departmentId}
              onChange={handleDepartmentChange}
            >
              {departments.map((department) => (
                <option key={department.value} value={department.value}>
                  {department.label}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className="form-group">
          <label htmlFor="employee-salary">Salario</label>
          <input
            id="employee-salary"
            type="number"
            min="0"
            step="0.01"
            value={formState.salary}
            onChange={handleSalaryChange}
            placeholder="Salario del empleado"
          />
        </div>

        <div className="button-row">
          <button type="submit" disabled={isSubmitting}>
            {isSubmitting
              ? "Guardando..."
              : isEditMode
                ? "Actualizar empleado"
                : "Crear empleado"}
          </button>

          {isEditMode && (
            <button
              type="button"
              className="secondary-button"
              onClick={handleCancel}
              disabled={isSubmitting}
            >
              Cancelar
            </button>
          )}
        </div>
      </form>
    </section>
  );
}

export default EmployeeForm;