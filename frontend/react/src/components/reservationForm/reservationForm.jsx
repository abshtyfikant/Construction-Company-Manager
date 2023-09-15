import classes from "./reservationForm.module.css";
import * as React from "react";
import { useNavigate, json } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faX } from "@fortawesome/free-solid-svg-icons";
import ErrorMsg from "../errorMsg";

export default function ReservationForm({ defaultValue, method }) {
  const token = localStorage.getItem("token");
  const [client, setClient] = React.useState(defaultValue?.client ?? undefined);
  const [city, setCity] = React.useState(defaultValue?.city ?? "");
  const [startDate, setStartDate] = React.useState(
    defaultValue?.beginDate.slice(0, 10) ?? ""
  );
  const [endDate, setEndDate] = React.useState(
    defaultValue?.endDate.slice(0, 10) ?? ""
  );
  const [serviceType, setServiceType] = React.useState(
    defaultValue?.serviceType ?? ""
  );
  const [workers, setWorkers] = React.useState(defaultValue?.workers ?? []);
  const [materials, setMaterials] = React.useState(
    defaultValue?.materials ?? []
  );
  const [resources, setResources] = React.useState(
    defaultValue?.resources ?? []
  );
  const [price, setPrice] = React.useState(defaultValue?.price ?? 0.0);
  const [fetchedWorkers, setFetchedWorkers] = React.useState();
  const [fetchedResources, setFetchedResources] = React.useState([]);
  const [fetchedClients, setFetchedClients] = React.useState();
  const [fetchedSpecializations, setFetchedSpecializations] = React.useState();
  const [fetchedAssignments, setFetchedAssignments] = React.useState();
  const [fetchedResAlloc, setFetchedResAlloc] = React.useState();
  const [currPopupType, setPopupType] = React.useState(null);
  const [popupOpen, setPopupOpen] = React.useState("");
  const resourceNameRef = React.useRef();
  const resourcePriceRef = React.useRef();
  const resourceAmountRef = React.useRef();
  const resourceUnitRef = React.useRef();
  const clientFirstNameRef = React.useRef();
  const clientLastNameRef = React.useRef();
  const clientCityRef = React.useRef();
  const [openDateErrorMsg, setOpenDateErrorMsg] = React.useState(false);
  const [tmpSpec, setTmpSpec] = React.useState();
  const [specName, setSpecName] = React.useState();
  const [tmpResource, setTmpResource] = React.useState({});
  const [paymentStatus, setPaymentStatus] = React.useState(
    defaultValue?.paymentStatus ?? ""
  );
  const [serviceStatus, setServiceStatus] = React.useState(
    defaultValue?.serviceStatus ?? ""
  );
  const navigate = useNavigate();

  const fetchDataBase = async (url, setFunction) => {
    try {
      const response = await fetch(url, {
        method: "get",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      });

      if (!response.ok) {
        alert(
          "Coś poszło nie tak przy pobieraniu danych. Spróbuj ponownie za chwilę."
        );
        throw json(
          { message: "Could not fetch data." },
          {
            status: 500,
          }
        );
      }

      const data = await response.json();
      setFunction(data);
    } catch (error) {
      alert(
        "Coś poszło nie tak przy pobieraniu danych. Spróbuj ponownie za chwilę."
      );
    }
  };

  const fetchData = async () => {
    fetchDataBase("https://localhost:7098/api/Employee", setFetchedWorkers);
    fetchDataBase(
      "https://localhost:7098/api/Assignment",
      setFetchedAssignments
    );
    fetchDataBase(
      "https://localhost:7098/api/Specialization",
      setFetchedSpecializations
    );
    fetchDataBase("https://localhost:7098/api/Resource", setFetchedResources);
    fetchDataBase("https://localhost:7098/api/Client", setFetchedClients);
    fetchDataBase(
      "https://localhost:7098/api/ResourceAllocation",
      setFetchedResAlloc
    );
  };

  React.useEffect(() => {
    fetchData();
  }, []);

  React.useEffect(() => {
    if (startDate.length === 0 || endDate >= startDate) {
      setOpenDateErrorMsg(false);
    } else {
      setOpenDateErrorMsg(true);
    }
  }, [startDate, endDate]);

  const submitClient = async () => {
    if (!client) {
      const tmpClient = {
        id: 0,
        firstName: clientFirstNameRef.current.value,
        lastName: clientLastNameRef.current.value,
        city: clientCityRef.current.value,
      };
      const response = await fetch("https://localhost:7098/api/Client", {
        method: method,
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
        body: JSON.stringify(tmpClient),
      });

      if (response.status === 422) {
        return response;
      }

      if (!response.ok) {
        alert(
          "Coś poszło nie tak przy dodawaniu rezerwacji. Spróbuj ponownie za chwilę."
        );
        throw json({ message: "Could not save client." }, { status: 500 });
      }
      const data = await response.json();
      return data;
    } else {
      return client;
    }
  };

  const submitReservation = async (clientId) => {
    const reservationData = {
      id: defaultValue?.id ?? 0,
      clientId: clientId,
      serviceType: serviceType,
      beginDate: startDate,
      endDate: endDate,
      city: city,
      price: price,
      serviceStatus: serviceStatus,
      paymentStatus: paymentStatus,
      assigments: workers.map((worker) => {
        return {
          id: worker.id ?? 0,
          employeeId: worker.employeeId,
          serviceId: 0,
          function: worker.function ?? "",
          startDate: worker.startDate,
          endDate: worker.endDate,
        };
      }),
      resources: resources.map((resource) => {
        return {
          id: resource.id ?? 0,
          resourceId: resource.resourceId,
          serviceId: 0,
          allocatedQuantity: resource.allocatedQuantity,
          beginDate: resource.beginDate,
          endDate: resource.endDate,
        };
      }),
      materials: materials.map((material) => {
        return {
          id: material.id ?? 0,
          serviceId: 0,
          name: material.name,
          unit: material.unit,
          price: material.price,
          quantity: material.quantity,
        };
      }),
    };

    const response = await fetch("https://localhost:7098/api/Service", {
      method: method,
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(reservationData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      alert(
        "Coś poszło nie tak przy dodawaniu rezerwacji. Spróbuj ponownie za chwilę."
      );
      throw json({ message: "Could not save reservation." }, { status: 500 });
      return;
    }
  };

  //Handle form submit - request
  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!openDateErrorMsg) {
      const submittedClient = await submitClient();
      if (submittedClient.id != undefined) {
        var submittedReservation = await submitReservation(submittedClient.id);
      } else {
        alert(
          "Coś poszło nie tak przy dodawaniu rezerwacji. Spróbuj ponownie za chwilę."
        );
        throw json({ message: "Something went wrong." }, { status: 500 });
      }
      return navigate("/rezerwacje");
    } else {
      return;
    }
  };

  const handlePopupOpen = (e, type) => {
    e.preventDefault();
    setPopupOpen(true);
    setPopupType(type);
  };

  const checkResAllocation = (resource) => {
    if (!resource) {
      return 0;
    }
    let availableQuant = resource.quantity;
    fetchedResAlloc?.map((resAlloc) => {
      if (resource.id === resAlloc.resourceId) {
        if (endDate >= resAlloc.beginDate && startDate <= resAlloc.endDate) {
          availableQuant -= resAlloc.allocatedQuantity;
          return availableQuant;
        }
      }
    });
    return availableQuant;
  };

  const checkAssignments = (worker) => {
    let isFree = true;
    fetchedAssignments?.map((assign) => {
      if (worker.id == assign.employeeId) {
        if (
          endDate >= assign.startDate.slice(0, 10) &&
          startDate <= assign.endDate.slice(0, 10)
        ) {
          isFree = false;
        }
      }
    });
    return isFree;
  };

  const handleAddMaterial = (e) => {
    e.preventDefault();
    if (
      resourceNameRef.current.value &&
      resourceUnitRef.current.value &&
      resourcePriceRef.current.value &&
      resourceAmountRef.current.value
    ) {
      const material = {
        id: 0,
        name: resourceNameRef.current.value,
        unit: resourceUnitRef.current.value,
        price: resourcePriceRef.current.value,
        quantity: resourceAmountRef.current.value,
      };
      setMaterials([...materials, material]);
      setPopupOpen(false);
    }
  };

  const handleAddWorker = () => {
    let tmpWorker = [];
    return (
      <div>
        <h1>Dodaj pracownika</h1>
        <select
          onChange={(e) => {
            setTmpSpec(e.target.value.split(" ")[0]);
            setSpecName(e.target.value.split(" ")[1]);
          }}
          className={classes.formInput}
          required
        >
          <option value="">Wybierz z listy</option>
          {fetchedSpecializations &&
            fetchedSpecializations.map((specialization) => {
              return (
                <option
                  key={specialization.id}
                  value={specialization.id + " " + specialization.name}
                >
                  {specialization.name}
                </option>
              );
            })}
        </select>
        <select
          onChange={(e) => {
            tmpWorker = fetchedWorkers.find((a) => a.id == e.target.value);
          }}
          className={classes.formInput}
          disabled={!tmpSpec}
          required
        >
          <option value="">Dostępni pracownicy</option>
          {fetchedWorkers &&
            fetchedWorkers.map((worker) => {
              if (
                checkAssignments(worker) &&
                tmpSpec == worker.mainSpecializationId &&
                !workers.find((existing) => existing.employeeId === worker.id)
              ) {
                return (
                  <option key={worker.id} value={worker.id}>
                    {worker.firstName} {worker.lastName}
                  </option>
                );
              } else {
                return (
                  <option disabled key={worker.id} value={worker.id}>
                    {worker.firstName} {worker.lastName}
                  </option>
                );
              }
            })}
        </select>
        <div className={classes.dateSection}>
          <label htmlFor="start-date-worker" className={classes.label}>
            Data od:
          </label>
          <input
            className={classes.formInput}
            id="start-date-worker"
            type="date"
            onChange={(e) =>
              (tmpWorker = { ...tmpWorker, startDate: e.target.value })
            }
            max={endDate}
            min={startDate}
            required
          />
          <label htmlFor="end-date-worker" className={classes.label}>
            Data do:
          </label>
          <input
            className={classes.formInput}
            id="end-date-worker"
            type="date"
            onChange={(e) => {
              if (startDate.length > 0 && e.target.value >= startDate) {
                tmpWorker = { ...tmpWorker, endDate: e.target.value };
              }
            }}
            min={startDate}
            max={endDate}
            required
          />
        </div>
        <button
          onClick={(e) => {
            e.preventDefault();
            if (
              tmpWorker?.id &&
              specName &&
              tmpWorker.startDate &&
              tmpWorker.endDate
            ) {
              setWorkers([
                {
                  id: undefined,
                  employeeId: tmpWorker.id,
                  serviceId: 0,
                  function: specName ?? "",
                  startDate: tmpWorker.startDate,
                  endDate: tmpWorker.endDate,
                  employee: tmpWorker.firstName + " " + tmpWorker.lastName,
                },
                ...workers,
              ]);
              setPopupOpen(false);
            }
          }}
        >
          Dodaj pracownika
        </button>
      </div>
    );
  };

  const handleAddResource = () => {
    return (
      <div>
        <h1>Dodaj zasoby</h1>
        <select
          onChange={(e) => {
            setTmpResource(
              fetchedResources.find((a) => a.id == e.target.value)
            );
          }}
          className={classes.formInput}
          required
        >
          <option value="">Wybierz z listy</option>
          {fetchedResources &&
            fetchedResources.map((resource) => {
              if (
                checkResAllocation(resource) > 0 &&
                !resources.find(
                  (existing) => existing.resourceId === resource.id
                )
              ) {
                return (
                  <option key={resource.id} value={resource.id}>
                    {resource.name}
                  </option>
                );
              } else {
                return (
                  <option disabled key={resource.id} value={resource.id}>
                    {resource.name}
                  </option>
                );
              }
            })}
        </select>
        <input
          type="number"
          onChange={(e) => {
            setTmpResource({ ...tmpResource, quantity: e.target.value });
          }}
          className={classes.formInput}
          disabled={Object.keys(tmpResource).length === 0}
          min="1"
          max={checkResAllocation(
            fetchedResources.find((resource) => resource.id === tmpResource.id)
          )}
          required
        ></input>
        <div className={classes.dateSection}>
          <label htmlFor="start-date-resource" className={classes.label}>
            Data od:
          </label>
          <input
            className={classes.formInput}
            id="start-date-resource"
            type="date"
            onChange={(e) =>
              setTmpResource({ ...tmpResource, startDate: e.target.value })
            }
            max={endDate}
            min={startDate}
            required
          />
          <label htmlFor="end-date-resource" className={classes.label}>
            Data do:
          </label>
          <input
            className={classes.formInput}
            id="end-date-resource"
            type="date"
            onChange={(e) => {
              if (startDate.length > 0 && e.target.value >= startDate) {
                setTmpResource({ ...tmpResource, endDate: e.target.value });
              }
            }}
            min={startDate}
            max={endDate}
            required
          />
        </div>
        <button
          onClick={(e) => {
            e.preventDefault();
            if (
              tmpResource.id &&
              tmpResource.quantity <=
                checkResAllocation(
                  fetchedResources.find(
                    (resource) => resource.id === tmpResource.id
                  )
                ) &&
              tmpResource.startDate &&
              tmpResource.endDate
            ) {
              setResources([
                {
                  id: undefined,
                  resourceId: tmpResource.id,
                  serviceId: 0,
                  allocatedQuantity: tmpResource.quantity,
                  beginDate: tmpResource.startDate,
                  endDate: tmpResource.endDate,
                  resourceName: tmpResource.name,
                },
                ...resources,
              ]);
              setPopupOpen(false);
              setTmpResource({});
            }
          }}
        >
          Dodaj zasób
        </button>
      </div>
    );
  };

  const handlePopup = () => {
    if (popupOpen) {
      switch (currPopupType) {
        case "workers":
          if (startDate && endDate) {
            return (
              <div className={classes.popupContainer}>{handleAddWorker()}</div>
            );
          } else {
            return (
              <div className={classes.popupContainer}>
                <h2>Najpierw dodaj daty rozpoczęcia i zakończenia usługi!</h2>
              </div>
            );
          }
        case "resources":
          if (startDate && endDate) {
            return (
              <div className={classes.popupContainer}>
                {handleAddResource()}
              </div>
            );
          } else {
            return (
              <div className={classes.popupContainer}>
                <h2>Najpierw dodaj daty rozpoczęcia i zakończenia usługi!</h2>
              </div>
            );
          }
        case "materials":
          return (
            <div className={classes.popupContainer}>
              <h1>Dodaj materiały</h1>
              <label htmlFor="resource-name" className={classes.label}>
                Nazwa:
              </label>
              <input
                className={classes.formInput}
                id="resource-name"
                type="text"
                ref={resourceNameRef}
                required
              />
              <label htmlFor="resource-price" className={classes.label}>
                Cena:
              </label>
              <input
                className={classes.formInput}
                id="resource-price"
                type="number"
                step=".01"
                ref={resourcePriceRef}
                required
              />
              <label htmlFor="resource-amount" className={classes.label}>
                Ilość:
              </label>
              <input
                className={classes.formInput}
                id="resource-amount"
                type="number"
                ref={resourceAmountRef}
                required
              />
              <label htmlFor="resource-unit" className={classes.label}>
                Jednostka:
              </label>
              <input
                className={classes.formInput}
                id="resource-unit"
                type="text"
                ref={resourceUnitRef}
                required
              />
              <button onClick={handleAddMaterial}>Dodaj materiał</button>
            </div>
          );
      }
    }
  };

  return (
    <form className={classes.formContainer}>
      <span
        className={classes.backdrop}
        style={{ display: popupOpen ? "block" : "none" }}
        onClick={() => setPopupOpen(false)}
      />
      {handlePopup()}
      <div className={classes.splitContainer}>
        <section className={classes.leftElements}>
          <h2>Usługa</h2>
          <label htmlFor="service-type" className={classes.label}>
            Typ usługi
          </label>
          <input
            className={classes.formInput}
            id="service-type"
            type="text"
            value={serviceType}
            placeholder="Dodaj typ usługi..."
            onChange={(e) => setServiceType(e.target.value)}
            required
          />
          <div className={classes.dateSection}>
            <label htmlFor="start-date" className={classes.label}>
              Data od:
            </label>
            <label htmlFor="end-date" className={classes.label}>
              Data do:
            </label>
            <input
              className={classes.formInput}
              id="start-date"
              type="date"
              value={startDate}
              onChange={(e) => setStartDate(e.target.value)}
              max={endDate}
              required
            />
            <input
              className={classes.formInput}
              id="end-date"
              type="date"
              value={endDate}
              onChange={(e) => {
                if (startDate.length > 0 && e.target.value >= startDate) {
                  setEndDate(e.target.value);
                } else {
                  setEndDate(null);
                }
              }}
              min={startDate}
              required
            />
          </div>
          <ErrorMsg
            isVisible={openDateErrorMsg}
            message="Data końcowa nie może być wcześniejsza niż data rozpoczęcia!"
          />
          <label htmlFor="city" className={classes.label}>
            Miasto:
          </label>
          <input
            className={classes.formInput}
            id="city"
            type="text"
            value={city}
            placeholder="Dodaj miasto..."
            onChange={(e) => setCity(e.target.value)}
            required
          />
          <label htmlFor="price" className={classes.label}>
            Cena za usługę:
          </label>
          <input
            className={classes.formInput}
            id="price"
            type="number"
            step=".01"
            value={price}
            placeholder="Dodaj cenę..."
            onInput={(e) => setPrice(e.target.value)}
            required
          />
          <label htmlFor="payment-status" className={classes.label}>
            Status płatności:
          </label>
          <select
            onChange={(e) => {
              setPaymentStatus(e.target.value);
            }}
            className={classes.formInput}
            required
            id="payment-status"
            value={paymentStatus}
          >
            <option value="">Wybierz z listy</option>
            <option value="zapłacone">zapłacone</option>
            <option value="niezapłacone">niezapłacone</option>
          </select>
          <label htmlFor="service-status" className={classes.label}>
            Status usługi:
          </label>
          <select
            onChange={(e) => {
              setServiceStatus(e.target.value);
            }}
            className={classes.formInput}
            required
            id="service-status"
            value={serviceStatus}
          >
            <option value="">Wybierz z listy</option>
            <option value="zaplanowana">zaplanowana</option>
            <option value="w trakcie wykonywania">w trakcie wykonywania</option>
            <option value="wykonana">wykonana</option>
            <option value="anulowana">anulowana</option>
          </select>
          <h2>Klient</h2>
          <p>Wybierz klienta z listy:</p>
          <select
            onChange={(e) => {
              setClient(fetchedClients.find((a) => a.id == e.target.value));
            }}
            className={classes.formInput}
            value={client?.id}
          >
            <option value="">Wybierz z listy</option>
            {fetchedClients &&
              fetchedClients.map((client) => {
                return (
                  <option key={client.id} value={client.id}>
                    {client.firstName} {client.lastName}
                  </option>
                );
              })}
          </select>
          <p>Lub dodaj nowego:</p>
          <label htmlFor="clientFirstName" className={classes.label}>
            Imię:
          </label>
          <input
            className={classes.formInput}
            id="clientFirstName"
            type="text"
            placeholder="Dodaj imię klienta..."
            ref={clientFirstNameRef}
            required
            disabled={client ? true : false}
          />
          <label htmlFor="clientLastName" className={classes.label}>
            Nazwisko:
          </label>
          <input
            className={classes.formInput}
            id="clientLastName"
            type="text"
            placeholder="Dodaj nazwisko klienta..."
            ref={clientLastNameRef}
            disabled={client ? true : false}
            required
          />
          <label htmlFor="clientCity" className={classes.label}>
            Miasto:
          </label>
          <input
            className={classes.formInput}
            id="clientCity"
            type="text"
            placeholder="Dodaj misto klienta..."
            ref={clientCityRef}
            disabled={client ? true : false}
            required
          />
        </section>
        <section className={classes.rightElements}>
          <div className={classes.actionSection}>
            <button
              className={classes.actionBtn}
              onClick={(e) => handlePopupOpen(e, "workers")}
            >
              + Dodaj pracownika
            </button>
            <p>Zespół wykonawczy:</p>
            Imię i nazwisko | Specjalizacja | Data od | Data do
            {workers && (
              <ul className={classes.list}>
                {workers.map((worker) => (
                  <li key={worker.id}>
                    {worker.employee} | {worker.function} | {worker.startDate} |{" "}
                    {worker.endDate}
                    <FontAwesomeIcon
                      icon={faX}
                      onClick={() => {
                        setWorkers(workers.filter((a) => a.id !== worker.id));
                      }}
                    />
                  </li>
                ))}
              </ul>
            )}
            {workers.length === 0 && <p>Brak danych</p>}
          </div>
          <div className={classes.actionSection}>
            <button
              className={classes.actionBtn}
              onClick={(e) => handlePopupOpen(e, "resources")}
            >
              + Dodaj zasoby
            </button>
            <p>Przydzielone zasoby:</p>
            <p>Nazwa | Ilość | Data od | Data do</p>
            {Object.keys(resources).length !== 0 && (
              <ul className={classes.list}>
                {resources.map((resource) => (
                  <li key={resource.id}>
                    {resource.resourceName} | {resource.allocatedQuantity} |{" "}
                    {resource.beginDate} | {resource.endDate}
                    <FontAwesomeIcon
                      icon={faX}
                      onClick={() => {
                        setResources(
                          resources.filter((a) => a.id !== resource.id)
                        );
                      }}
                    />
                  </li>
                ))}
              </ul>
            )}
            {resources.length === 0 && <p>Brak danych</p>}
          </div>
          <div className={classes.actionSection}>
            <button
              className={classes.actionBtn}
              onClick={(e) => handlePopupOpen(e, "materials")}
            >
              + Dodaj materiały
            </button>
            <p>Materiały:</p>
            <p>Nazwa | Cena | Ilość | Jednostka </p>
            {Object.keys(materials).length !== 0 && (
              <ul className={classes.list}>
                {materials.map((material) => (
                  <li key={material.id}>
                    {material.name} | {material.price} | {material.quantity} |{" "}
                    {material.unit}
                    <FontAwesomeIcon
                      icon={faX}
                      onClick={() => {
                        setMaterials(
                          materials.filter((a) => a.id !== material.id)
                        );
                      }}
                    />
                  </li>
                ))}
              </ul>
            )}
            {materials.length === 0 && <p>Brak danych</p>}
          </div>
        </section>
      </div>
      <button
        type="submit"
        className={classes.submitBtn}
        onClick={handleSubmit}
      >
        Utwórz rezerwację
      </button>
    </form>
  );
}
