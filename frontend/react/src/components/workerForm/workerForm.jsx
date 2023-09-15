import "./workerForm.css";
import * as React from "react";
import { useNavigate, json } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from "@fortawesome/free-solid-svg-icons";

export default function WorkerForm({ defaultValue, method }) {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const firstNameRef = React.useRef();
  const lastNameRef = React.useRef();
  const cityRef = React.useRef();
  const hourlyRateRef = React.useRef();
  const [specialization, setSpecialization] = React.useState(
    defaultValue
      ? defaultValue.mainSpecializationId
        ? defaultValue.mainSpecializationId
        : undefined
      : undefined
  );
  const [fetchedSpecializations, setFetchedSpecializations] = React.useState();
  const [allocation, setAllocation] = React.useState([]);

  const fetchData = React.useCallback(async () => {
    try {
      const response = await fetch(
        "https://localhost:7098/api/Specialization",
        {
          method: "get",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,
          },
        }
      );
      if (!response.ok) {
        throw new Error("Something went wrong!");
      }

      const data = await response.json();
      setFetchedSpecializations(data);
    } catch (error) {
      //setError("Something went wrong, try again.");
    }
  }, []);

  const handleSubmit = async (event) => {
    event.preventDefault();
    const workerData = {
      id: method === "put" ? defaultValue.id : 0,
      firstName: firstNameRef.current.value,
      lastName: lastNameRef.current.value,
      city: cityRef.current.value,
      ratePerHour: Number(hourlyRateRef.current.value),
      mainSpecializationId: specialization,
    };

    const response = await fetch("https://localhost:7098/api/Employee", {
      method: method,
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(workerData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      throw json({ message: "Could not save worker." }, { status: 500 });
    }

    return navigate("/pracownicy"); // Przekierowanie po dodaniu pracownika
  };

  const handleDelete = async (e) => {
    const response = await fetch(
      "https://localhost:7098/api/Employee/" + defaultValue.id,
      {
        method: "delete",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    );

    if (!response.ok) {
      throw json(
        { message: "Could not fetch resource." },
        {
          status: 500,
        }
      );
    } else {
      return navigate("/pracownicy");
    }
  };

  const getAlloc = async () => {
    try {
      const response = await fetch(
        "https://localhost:7098/api/Assignment/GetEmployeeAssignments/" +
          defaultValue.id,
        {
          method: "get",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,
          },
        }
      );

      if (!response.ok) {
        throw json(
          { message: "Could not fetch reports." },
          {
            status: 500,
          }
        );
      }

      const data = await response.json();
      setAllocation(data);
    } catch (error) {
      //setError("Something went wrong, try again.");
    }
  };

  React.useEffect(() => {
    fetchData();
    getAlloc();
  }, []);

  const checkAlloc = () => {
    if (allocation.length > 0) {
      return true;
    } else {
      return false;
    }
  };

  return (
    <div className="worker-form">
      <div className="container">
        <form onSubmit={handleSubmit}>
          <div className="label-container">
            <div className="column">
              <label>
                Imię:
                <input
                  type="text"
                  name="firstName"
                  ref={firstNameRef}
                  defaultValue={defaultValue ? defaultValue.firstName : null}
                  disabled={method === "put" ? true : false}
                  required
                />
              </label>
              <label>
                Nazwisko:
                <input
                  type="text"
                  name="lastName"
                  ref={lastNameRef}
                  defaultValue={defaultValue ? defaultValue.lastName : null}
                  disabled={method === "put" ? true : false}
                  required
                />
              </label>
              <label>
                Miasto:
                <input
                  type="text"
                  name="city"
                  ref={cityRef}
                  defaultValue={defaultValue ? defaultValue.city : null}
                  required
                />
              </label>
              <label>Specjalizacja:</label>
              <select
                onChange={(e) => {
                  setSpecialization(e.target.value);
                }}
                value={specialization}
                required
              >
                <option value="">Wybierz z listy</option>
                {fetchedSpecializations &&
                  fetchedSpecializations.map((specialization) => {
                    return (
                      <option key={specialization.id} value={specialization.id}>
                        {specialization.name}
                      </option>
                    );
                  })}
              </select>
              <label>
                Stawka godzinowa (zł/h):
                <input
                  type="number"
                  step="0.01"
                  name="hourlyRate"
                  ref={hourlyRateRef}
                  defaultValue={defaultValue ? defaultValue.ratePerHour : null}
                  required
                />
              </label>
            </div>
          </div>
          <div className="button-container">
            <button type="submit">Dodaj pracownika</button>
            {method === "put" && (
              <button
                onClick={(e) => {
                  handleDelete(e);
                }}
                disabled={checkAlloc() ? true : false}
              >
                <FontAwesomeIcon icon={faTrash} />
              </button>
            )}
          </div>
        </form>
      </div>
    </div>
  );
}