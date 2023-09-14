import GridMenuHeader from "../components/gridMenuHeader";
import "../css/add-specialization.css";
import * as React from "react";
import { useNavigate, json } from "react-router-dom";

export default function AddSpecialization() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const nameRef = React.useRef();

  const handleSubmit = async (event) => {
    event.preventDefault();
    const specData = {
      id: 0,
      name: nameRef.current.value,
    };

    const response = await fetch("https://localhost:7098/api/Specialization", {
      method: "post",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(specData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      throw json({ message: "Could not save worker." }, { status: 500 });
    }
    const data = await response.json();
    return navigate("/menu"); // Przekierowanie po dodaniu pracownika
  };

  return (
    <div className="add-specialization">
      <GridMenuHeader headerTitle="Dodaj specjalizację" />
      <div className="container">
        <form onSubmit={handleSubmit}>
          <div className="label-container">
            <div className="column">
              <label>
                Nazwa specjalizacji:
                <input type="text" name="name" ref={nameRef} />
              </label>
            </div>
          </div>
          <div className="button-container">
            <button type="submit">Dodaj specjalizację</button>
          </div>
        </form>
      </div>
    </div>
  );
}
