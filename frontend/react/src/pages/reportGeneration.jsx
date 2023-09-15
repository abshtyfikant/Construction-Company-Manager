import GridMenuHeader from "../components/gridMenuHeader";
import classes from "./styles/reportGeneration.module.css";
import * as React from "react";
import { useNavigate, json, useLocation } from "react-router-dom";

const types = [
  "raport o zarobkach firmy",
  "raport o zarobkach pracownika",
  "raport o kosztach",
  "raport z usługi",
];

function ReportGeneration() {
  const location = useLocation();
  const navigate = useNavigate();
  const token = localStorage.getItem("token");
  const userId = localStorage.getItem("userId");
  const [description, setDescription] = React.useState("");
  const [city, setCity] = React.useState("");
  const [startDate, setStartDate] = React.useState("");
  const [endDate, setEndDate] = React.useState("");
  const [reportType, setReportType] = React.useState(
    location.state?.reservation ? "raport z usługi" : ""
  );
  const [selectedVal, setSelectedVal] = React.useState(
    location.state?.reservation?.id ?? ""
  );
  const [fetchedWorkers, setFetchedWorkers] = React.useState([]);
  const [fetchedServices, setFetchedServices] = React.useState([]);
  const fetchData = React.useCallback(async () => {
    try {
      const response = await fetch("https://localhost:7098/api/Employee", {
        method: "get",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      });

      if (!response.ok) {
        throw new Error("Something went wrong!");
      }

      const data = await response.json();
      setFetchedWorkers(data);
    } catch (error) {
      //setError("Something went wrong, try again.");
    }

    try {
      const response = await fetch("https://localhost:7098/api/Service", {
        method: "get",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (!response.ok) {
        throw new Error("Something went wrong!");
      }

      const data = await response.json();
      setFetchedServices(data);
    } catch (error) {
      //setError("Something went wrong, try again.");
    }
  }, []);

  React.useEffect(() => {
    fetchData();
  }, []);

  React.useEffect(() => {
    if (reportType !== "raport z usługi") {
      return;
    }
    const foundService = fetchedServices?.find(
      (service) => service.id == selectedVal
    );
    setStartDate(foundService?.beginDate);
    setEndDate(foundService?.endDate);
  }, [selectedVal, reportType]);

  const renderSelect = () => {
    switch (reportType) {
      case "raport o zarobkach firmy":
        return (
          <select
            className={classes.formInput}
            id="worker"
            onChange={(e) => setSelectedVal(e.target.value)}
            disabled
          >
            <option value="">Wybierz z listy</option>
          </select>
        );

      case "raport o zarobkach pracownika":
        return (
          <select
            className={classes.formInput}
            id="worker"
            onChange={(e) => setSelectedVal(e.target.value)}
            required
          >
            <option value="">Wybierz z listy</option>
            {fetchedWorkers &&
              fetchedWorkers.map((worker) => (
                <option key={worker.id} value={worker.id}>
                  {worker.firstName} {worker.lastName}
                </option>
              ))}
          </select>
        );

      case "raport o kosztach":
        return (
          <select
            className={classes.formInput}
            id="worker"
            onChange={(e) => setSelectedVal(e.target.value)}
            disabled
          >
            <option value="">Wybierz z listy</option>
          </select>
        );

      case "raport z usługi":
        return (
          <select
            className={classes.formInput}
            id="reservation"
            onChange={(e) => setSelectedVal(e.target.value)}
            required
            value={selectedVal}
          >
            <option value="">Wybierz z listy</option>
            {fetchedServices &&
              fetchedServices.map((service) => (
                <option key={service.id} value={service.id}>
                  {service.serviceType}
                </option>
              ))}
          </select>
        );
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    let url = "";
    let amount = 0;

    switch (reportType) {
      case "raport o zarobkach pracownika":
        url = `https://localhost:7098/api/Employee/GetEmployeeEarnings/${startDate}/${endDate}/${selectedVal}`;
        break;
      case "raport o kosztach":
        url = `https://localhost:7098/api/Employee/GetEmployeesEarnings/${startDate}/${endDate}`;
        const response1 = await fetch(url, {
          headers: {
            method: "get",
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,
          },
        });
        if (response1.ok) {
          amount += await response1.json();
        } else {
          alert(
            "Wystąpił błąd podczas ładowania danych. Spróbuj ponownie za chwilę."
          );
          return;
        }

        url = `https://localhost:7098/api/Material/GetCostInTime/${startDate}/${endDate}`;
        break;
      case "raport o zarobkach firmy":
        url = `https://localhost:7098/api/Service/GetIncome/${startDate}/${endDate}`;
        break;
      case "raport z usługi":
        url = `https://localhost:7098/api/Service/GetCost/${selectedVal}`;
        break;
    }
    alert("before sending")
    const response1 = await fetch(url, {
      headers: {
        method: "get",
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    });

    if (response1.ok) {
      amount += await response1.json();
    } else {
      alert(
        "Wystąpił błąd podczas ładowania danych. Spróbuj ponownie za chwilę."
      );
      return;
    }

    let reportData;

    switch (reportType) {
      case "raport o zarobkach pracownika":
        reportData = {
          id: 0,
          serviceId: null,
          employeeId: selectedVal,
          reportType: reportType,
          description: description,
          beginDate: startDate.length > 10 ? startDate.slice(0, 10) : startDate,
          endDate: endDate.length > 10 ? endDate.slice(0, 10) : endDate,
          amount: amount,
          city: city,
          userId: userId,
        };
        break;
      case "raport o kosztach":
      case "raport o zarobkach firmy":
        reportData = {
          id: 0,
          serviceId: null,
          employeeId: null,
          reportType: reportType,
          description: description,
          beginDate: startDate.length > 10 ? startDate.slice(0, 10) : startDate,
          endDate: endDate.length > 10 ? endDate.slice(0, 10) : endDate,
          amount: amount,
          city: city,
          userId: userId,
        };
        break;
      case "raport z usługi":
        reportData = {
          id: 0,
          serviceId: selectedVal,
          employeeId: null,
          reportType: reportType,
          description: description,
          beginDate: startDate.length > 10 ? startDate.slice(0, 10) : startDate,
          endDate: endDate.length > 10 ? endDate.slice(0, 10) : endDate,
          amount: amount,
          city: city,
          userId: userId,
        };
        break;
    }
    console.log(reportData);
    const response = await fetch(`https://localhost:7098/api/Report`, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify(reportData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      throw json({ message: "Could not save reservation." }, { status: 500 });
    }

    return navigate("/raporty");
  };

  return (
    <div className={classes.container}>
      <GridMenuHeader headerTitle="Generowanie raportu" />
      <form onSubmit={handleSubmit} className={classes.formContainer}>
        <div className={classes.formContent}>
          <div className={classes.splitSection}>
            <label htmlFor="report-type" className={classes.label}>
              Rodzaj raportu:
            </label>
            {reportType === "raport o zarobkach pracownika" ||
            reportType === "raport z usługi" ? (
              <label htmlFor="worker" className={classes.label}>
                Wybierz{" "}
                {reportType === "raport o zarobkach pracownika"
                  ? "pracownika"
                  : "usługę"}
              </label>
            ) : (
              <br></br>
            )}
            <select
              className={classes.formInput}
              id="report-type"
              onChange={(e) => setReportType(e.target.value)}
              reqiured
              value={reportType}
            >
              <option value="">Wybierz z listy</option>
              {types.map((type) => (
                <option key={type} value={type}>
                  {type}
                </option>
              ))}
            </select>

            {renderSelect()}
          </div>
          <label htmlFor="start-date" className={classes.label}>
            Data od:
          </label>
          <input
            className={classes.formInput}
            id="start-date"
            type="date"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            required={reportType === "raport z usługi" ? false : true}
            disabled={reportType === "raport z usługi" ? true : false}
          />
          <label htmlFor="end-date" className={classes.label}>
            Data do:
          </label>
          <input
            className={classes.formInput}
            id="end-date"
            type="date"
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
            required={reportType === "raport z usługi" ? false : true}
            disabled={reportType === "raport z usługi" ? true : false}
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
            required
            onChange={(e) => setCity(e.target.value)}
          />
          <label htmlFor="description" className={classes.label}>
            Opis:
          </label>
          <input
            className={classes.formInput}
            id="description"
            type="text"
            value={description}
            placeholder="Dodaj opis raportu..."
            required
            onChange={(e) => setDescription(e.target.value)}
          />
        </div>
        <button type="submit" className={classes.submitBtn}>
          Generuj raport
        </button>
      </form>
    </div>
  );
}

export default ReportGeneration;
