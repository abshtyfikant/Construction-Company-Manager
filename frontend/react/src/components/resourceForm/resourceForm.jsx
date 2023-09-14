import "./resourceForm.css";
import { useNavigate, json } from "react-router-dom";
import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from "@fortawesome/free-solid-svg-icons";

export default function ResourceForm({ defaultValue, method }) {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const quantityRef = React.useRef();
  const nameRef = React.useRef();
  const [allocation, setAllocation] = React.useState([]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formData = {
      id: defaultValue?.id ?? 0,
      name: nameRef.current.value,
      quantity: Number(quantityRef.current.value),
    };

    const response = await fetch("https://localhost:7098/api/Resource", {
      method: method,
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(formData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      throw json({ message: "Could not save reservation." }, { status: 500 });
    }

    return navigate("/stan-zasobow");
  };

  const handleDelete = async (e) => {
    const response = await fetch(
      "https://localhost:7098/api/Resource/" + defaultValue.id,
      {
        method: "delete",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    );

    if (!response.ok) {
      // return { isError: true, message: 'Could not fetch project.' };
      // throw new Response(JSON.stringify({ message: 'Could not fetch project.' }), {
      //   status: 500,
      // });
      throw json(
        { message: "Could not fetch resource." },
        {
          status: 500,
        }
      );
    } else {
      return navigate("/stan-zasobow");
    }
  };

  const getAlloc = async () => {
    try {
      const response = await fetch(
        "https://localhost:7098/api/ResourceAllocation/Resource/" +
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
          { message: "Could not fetch resource." },
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
    <div>
      <div className="container">
        <form onSubmit={handleSubmit}>
          <div className="label-container">
            <div className="column">
              <label>
                Nazwa zasobu:
                <input
                  type="text"
                  name="resourceName"
                  id="resourceName"
                  ref={nameRef}
                  defaultValue={defaultValue ? defaultValue.name : null}
                  required
                />
              </label>
              <label htmlFor="quantity"> Ilość: </label>
              <input
                type="number"
                name="quantity"
                id="quantity"
                ref={quantityRef}
                defaultValue={defaultValue ? defaultValue.quantity : null}
                required
              />
            </div>
          </div>
          <div className="button-container">
            <button type="submit">Zatwierdź</button>
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
