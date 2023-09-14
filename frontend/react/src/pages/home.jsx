import { Link } from "react-router-dom";

export default function HomePage() {
  return (
    <div className="start-page">
      <h1>Witaj</h1>
      <Link to="/login">Zaloguj się</Link>
      <Link to="/zarejestruj-sie">Zarejestruj się</Link>
    </div>
  );
}
