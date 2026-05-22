import apiClient from "./apiClient";
import type {
  CreateEmployeeRequest,
  EmployeeResponse,
  UpdateEmployeeRequest,
} from "../models/employee";

export async function getEmployees(): Promise<EmployeeResponse[]> {
  const response = await apiClient.get<EmployeeResponse[]>("/employees");
  return response.data;
}

export async function getEmployeeById(id: number): Promise<EmployeeResponse> {
  const response = await apiClient.get<EmployeeResponse>(`/employees/${id}`);
  return response.data;
}

export async function createEmployee(
  request: CreateEmployeeRequest
): Promise<EmployeeResponse> {
  const response = await apiClient.post<EmployeeResponse>("/employees", request);
  return response.data;
}

export async function updateEmployee(
  id: number,
  request: UpdateEmployeeRequest
): Promise<EmployeeResponse> {
  const response = await apiClient.put<EmployeeResponse>(`/employees/${id}`, request);
  return response.data;
}

export async function deleteEmployee(id: number): Promise<void> {
  await apiClient.delete(`/employees/${id}`);
}