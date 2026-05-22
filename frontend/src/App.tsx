import { useState } from "react";
import "./App.css";
import LoginForm from "./features/auth/LoginForm";
import RegisterForm from "./features/auth/RegisterForm";
import EmployeesPage from "./features/employees/EmployeesPage";
import type { AuthResponse } from "./models/auth";

type AuthView = "login" | "register";

function getStoredUser(): AuthResponse | null {
  const storedUser = localStorage.getItem("authUser");

  if (!storedUser) {
    return null;
  }

  try {
    return JSON.parse(storedUser) as AuthResponse;
  } catch {
    localStorage.removeItem("authUser");
    localStorage.removeItem("authToken");
    return null;
  }
}

function App() {
  const [authUser, setAuthUser] = useState<AuthResponse | null>(getStoredUser());
  const [authView, setAuthView] = useState<AuthView>("login");

  function handleAuthSuccess(auth: AuthResponse) {
    setAuthUser(auth);
  }

  function handleLogout() {
    localStorage.removeItem("authUser");
    localStorage.removeItem("authToken");
    setAuthUser(null);
    setAuthView("login");
  }

  if (!authUser) {
    return (
      <main className="page centered-page">
        {authView === "login" ? (
          <LoginForm
            onLoginSuccess={handleAuthSuccess}
            onShowRegister={() => setAuthView("register")}
          />
        ) : (
          <RegisterForm
            onRegisterSuccess={handleAuthSuccess}
            onShowLogin={() => setAuthView("login")}
          />
        )}
      </main>
    );
  }

  return (
    <main className="page">
      <header className="app-header">
        <div>
          <h1>Gestión de Empleados</h1>
          <p className="muted-text">
            Sesión iniciada como {authUser.fullName} · Rol: {authUser.role}
          </p>
        </div>

        <button type="button" className="secondary-button" onClick={handleLogout}>
          Cerrar sesión
        </button>
      </header>

      <EmployeesPage authUser={authUser} />
    </main>
  );
}

export default App;