import React from "react";
import { Link } from "react-router-dom";

function NotFound() {
  return (
    <section className="menu">
      <div className="grid-3">
        <div className="item"></div>
        <div className="item">
          <h1>Taka strona nie istnieje</h1>
        </div>
      </div>
      <section className="center-container">
        <nav>
          <div>
            <Link className="menu-element" to="/login">
              Wróć do logowania
            </Link>
          </div>
          <div>
            <Link className="menu-element" to="/zarejestruj-sie">
              Wróć do rejestracji
            </Link>
          </div>
          <div>
            <Link className="menu-element" to="/menu">
              Wróć do menu
            </Link>
          </div>
        </nav>
      </section>
    </section>
  );
}

export default NotFound;
