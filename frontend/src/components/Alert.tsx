interface AlertProps {
  type: "success" | "error" | "info";
  message: string;
}

function Alert({ type, message }: AlertProps) {
  return <div className={`alert alert-${type}`}>{message}</div>;
}

export default Alert;