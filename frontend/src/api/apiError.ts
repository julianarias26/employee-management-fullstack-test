import axios from "axios";

interface ApiErrorResponse {
  message?: string;
  title?: string;
  errors?: Record<string, string[]>;
}

export function getApiErrorMessage(error: unknown): string {
  if (!axios.isAxiosError<ApiErrorResponse>(error)) {
    return "Unexpected error. Please try again.";
  }

  if (error.response?.status === 401) {
    return "You are not authenticated. Please sign in again.";
  }

  if (error.response?.status === 403) {
    return "You do not have permission to perform this action.";
  }

  const responseData = error.response?.data;

  if (responseData?.message) {
    return responseData.message;
  }

  if (responseData?.title) {
    return responseData.title;
  }

  if (responseData?.errors) {
    const firstError = Object.values(responseData.errors)[0]?.[0];

    if (firstError) {
      return firstError;
    }
  }

  return "Request failed. Please try again.";
}