import { useState } from "react";
import type { FormEvent } from "react";
import { register } from "../../api/authService";
import { getApiErrorMessage } from "../../api/apiError";
import Alert from "../../components/Alert";
import LoadingMessage from "../../components/LoadingMessage";
import type { AuthResponse, UserRole } from "../../models/auth";

interface RegisterFormProps {
  onRegisterSuccess: (auth: AuthResponse) => void;
  onShowLogin: () => void;
}

function RegisterForm({ onRegisterSuccess, onShowLogin }: RegisterFormProps) {
  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState<UserRole>("User");
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setErrorMessage("");

    if (!fullName.trim()) {
      setErrorMessage("Nombre es requerido.");
      return;
    }

    if (!email.trim()) {
      setErrorMessage("Email es requerido.");
      return;
    }

    if (!password.trim()) {
      setErrorMessage("Contraseña es requerida.");
      return;
    }

    if (password.length < 6) {
      setErrorMessage("Contraseña debe tener al menos 6 caracteres.");
      return;
    }

    try {
      setIsLoading(true);

      const authResponse = await register({
        fullName: fullName.trim(),
        email: email.trim(),
        password,
        role,
      });

      localStorage.setItem("authToken", authResponse.token);
      localStorage.setItem("authUser", JSON.stringify(authResponse));

      onRegisterSuccess(authResponse);
    } catch (error) {
      setErrorMessage(getApiErrorMessage(error));
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <section className="card login-card">
      <h1>Crear cuenta</h1>
      <p className="muted-text">Registra un usuario para acceder al sistema.</p>

      {errorMessage && <Alert type="error" message={errorMessage} />}

      <form onSubmit={handleSubmit} className="form">
        <div className="form-group">
          <label htmlFor="fullName">Nombre</label>

          <input
            id="fullName"
            type="text"
            value={fullName}
            onChange={(event) => setFullName(event.target.value)}
            placeholder="Kevin Arias"
          />
        </div>

        <div className="form-group">
          <label htmlFor="register-email">Email</label>

          <input
            id="register-email"
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            placeholder="admin@test.com"
          />
        </div>

        <div className="form-group">
          <label htmlFor="register-password">Contraseña</label>

          <input
            id="register-password"
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            placeholder="Mínimo 6 caracteres"
          />
        </div>

        <div className="form-group">
          <label htmlFor="role">Rol</label>

          <select
            id="role"
            value={role}
            onChange={(event) => setRole(event.target.value as UserRole)}
          >
            <option value="User">User</option>
            <option value="Admin">Admin</option>
          </select>
        </div>

        <button type="submit" disabled={isLoading}>
          {isLoading ? "Creando cuenta..." : "Crear cuenta"}
        </button>
      </form>

      {isLoading && <LoadingMessage message="Registrando usuario..." />}

      <p className="muted-text auth-switch-text">
        ¿Ya tienes una cuenta?{" "}
        <button type="button" className="link-button" onClick={onShowLogin}>
          Iniciar sesión
        </button>
      </p>
    </section>
  );
}

export default RegisterForm;