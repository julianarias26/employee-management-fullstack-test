export interface PositionHistoryResponse {
  id: number;
  position: string;
  startDate: string;
  endDate: string | null;
}

export interface EmployeeResponse {
  id: number;
  name: string;
  currentPosition: number;
  currentPositionName: string;
  salary: number;
  annualBonus: number;
  departmentId: number;
  departmentName: string;
  projects: string[];
  positionHistories: PositionHistoryResponse[];
}

export interface CreateEmployeeRequest {
  name: string;
  currentPosition: number;
  salary: number;
  departmentId: number;
}

export interface UpdateEmployeeRequest {
  name: string;
  currentPosition: number;
  salary: number;
  departmentId: number;
}