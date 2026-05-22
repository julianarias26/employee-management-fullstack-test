import { useState } from "react";
import type { FormEvent } from "react";
import { login } from "../../api/authService";
import Alert from "../../components/Alert";
import LoadingMessage from "../../components/LoadingMessage";
import type { AuthResponse } from "../../models/auth";

interface LoginFormProps {
  onLoginSuccess: (auth: AuthResponse) => void;
  onShowRegister: () => void;
}

function LoginForm({ onLoginSuccess, onShowRegister }: LoginFormProps) {
  const [email, setEmail] = useState("admin@test.com");
  const [password, setPassword] = useState("Admin123");
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setErrorMessage("");

    if (!email.trim()) {
      setErrorMessage("Email es requerido.");
      return;
    }

    if (!password.trim()) {
      setErrorMessage("Contraseña es requerida.");
      return;
    }

    try {
      setIsLoading(true);

      const authResponse = await login({
        email,
        password,
      });

      localStorage.setItem("authToken", authResponse.token);
      localStorage.setItem("authUser", JSON.stringify(authResponse));

      onLoginSuccess(authResponse);
    } catch {
      setErrorMessage("Email o contraseña inválidos.");
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <section className="card login-card">
      <h1>Gestión de Empleados</h1>
      <p className="muted-text">Inicia sesión para gestionar empleados.</p>

      {errorMessage && <Alert type="error" message={errorMessage} />}

      <form onSubmit={handleSubmit} className="form">
        <div className="form-group">
          <label htmlFor="email">Email</label>

          <input
            id="email"
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            placeholder="admin@test.com"
          />
        </div>

        <div className="form-group">
          <label htmlFor="password">Contraseña</label>

          <input
            id="password"
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            placeholder="Admin123"
          />
        </div>

        <button type="submit" disabled={isLoading}>
          {isLoading ? "Iniciando sesión..." : "Iniciar sesión"}
        </button>
      </form>

      {isLoading && <LoadingMessage message="Validando credenciales..." />}

      <p className="muted-text auth-switch-text">
        ¿No tienes una cuenta?{" "}
        <button type="button" className="link-button" onClick={onShowRegister}>
          Crear cuenta
        </button>
      </p>
    </section>
  );
}

export default LoginForm;