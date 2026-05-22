import { useEffect, useState } from "react";
import {
  createEmployee,
  deleteEmployee,
  getEmployees,
  updateEmployee,
} from "../../api/employeeService";
import { getApiErrorMessage } from "../../api/apiError";
import Alert from "../../components/Alert";
import LoadingMessage from "../../components/LoadingMessage";
import type { AuthResponse } from "../../models/auth";
import type {
  CreateEmployeeRequest,
  EmployeeResponse,
  UpdateEmployeeRequest,
} from "../../models/employee";
import EmployeeForm from "./EmployeeForm";
import EmployeeTable from "./EmployeeTable";

interface EmployeesPageProps {
  authUser: AuthResponse;
}

function EmployeesPage({ authUser }: EmployeesPageProps) {
  const [employees, setEmployees] = useState<EmployeeResponse[]>([]);
  const [selectedEmployee, setSelectedEmployee] = useState<EmployeeResponse | null>(
    null
  );
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [message, setMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const canManageEmployees = authUser.role === "Admin";

  useEffect(() => {
    loadEmployees();
  }, []);

  async function loadEmployees() {
    try {
      setIsLoading(true);
      setErrorMessage("");

      const employeesFromApi = await getEmployees();

      setEmployees(employeesFromApi);
    } catch (error) {
      setErrorMessage(getApiErrorMessage(error));
    } finally {
      setIsLoading(false);
    }
  }

  async function handleCreate(request: CreateEmployeeRequest) {
    try {
      setIsSubmitting(true);
      setErrorMessage("");
      setMessage("");

      const createdEmployee = await createEmployee(request);

      setEmployees((currentEmployees) => [...currentEmployees, createdEmployee]);
      setMessage("Empleado creado exitosamente.");
    } catch (error) {
      setErrorMessage(getApiErrorMessage(error));
    } finally {
      setIsSubmitting(false);
    }
  }

  async function handleUpdate(id: number, request: UpdateEmployeeRequest) {
    try {
      setIsSubmitting(true);
      setErrorMessage("");
      setMessage("");

      const updatedEmployee = await updateEmployee(id, request);

      setEmployees((currentEmployees) =>
        currentEmployees.map((employee) =>
          employee.id === id ? updatedEmployee : employee
        )
      );

      setSelectedEmployee(null);
      setMessage("Empleado actualizado exitosamente.");
    } catch (error) {
      setErrorMessage(getApiErrorMessage(error));
    } finally {
      setIsSubmitting(false);
    }
  }

  async function handleDelete(id: number) {
    const shouldDelete = window.confirm(
      "¿Estás seguro de que quieres eliminar este empleado?"
    );

    if (!shouldDelete) {
      return;
    }

    try {
      setErrorMessage("");
      setMessage("");

      await deleteEmployee(id);

      setEmployees((currentEmployees) =>
        currentEmployees.filter((employee) => employee.id !== id)
      );

      if (selectedEmployee?.id === id) {
        setSelectedEmployee(null);
      }

      setMessage("Empleado eliminado exitosamente.");
    } catch (error) {
      setErrorMessage(getApiErrorMessage(error));
    }
  }

  function handleEdit(employee: EmployeeResponse) {
    setSelectedEmployee(employee);
    setMessage("");
    setErrorMessage("");
  }

  function handleCancelEdit() {
    setSelectedEmployee(null);
  }

  return (
    <>
      {message && <Alert type="success" message={message} />}
      {errorMessage && <Alert type="error" message={errorMessage} />}

      {!canManageEmployees && (
        <Alert
          type="info"
          message="No tienes permisos para crear, editar o eliminar empleados."
        />
      )}

      {canManageEmployees && (
        <EmployeeForm
          selectedEmployee={selectedEmployee}
          isSubmitting={isSubmitting}
          onCreate={handleCreate}
          onUpdate={handleUpdate}
          onCancelEdit={handleCancelEdit}
        />
      )}

      {isLoading ? (
        <LoadingMessage message="Cargando empleados..." />
      ) : (
        <EmployeeTable
          employees={employees}
          canManageEmployees={canManageEmployees}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      )}
    </>
  );
}

export default EmployeesPage;