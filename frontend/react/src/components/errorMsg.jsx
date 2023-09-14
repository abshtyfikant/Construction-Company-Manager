import "./errorMsg.css";

function ErrorMsg({ isVisible, message }) {
  return (
    <div className={`error-msg ${isVisible ? "visible" : "hidden"}`}>
      <p>{message}</p>
    </div>
  );
}

export default ErrorMsg;
