import "../../css/report-details.css";
import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import GridMenuHeader from "../gridMenuHeader";
import jsPDF from "jspdf";

function ServiceReport() {
  const { reportId } = useParams();
  const [report, setReport] = useState(null); // Dane raportu z API
  const [service, setService] = useState(null);

  // Funkcja pobierająca dane raportu z API
  async function fetchReportDetails(id) {
    const token = localStorage.getItem("token");
    const response = await fetch(`https://localhost:7098/api/Report/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    });
    let data;
    if (response.ok) {
      data = await response.json();
      setReport(data);
    } else {
      return null;
    }

    const response2 = await fetch(
      `https://localhost:7098/api/Service/${data.serviceId}`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    );

    if (response2.ok) {
      const data2 = await response2.json();
      setService(data2);
    } else {
      return null;
    }
  }

  // Efekt, który pobiera dane raportu z API przy ładowaniu komponentu
  useEffect(() => {
    fetchReportDetails(reportId);
  }, [reportId]);

  // Obsługa przypadku, gdy dane raportu są jeszcze ładowane
  if (!report) {
    return <div>Ładowanie...</div>;
  }

  const generatePDF = () => {
    const element = document.getElementById("service-report-pdf");

    if (element) {
      const pdf = new jsPDF();
      pdf.text(element.innerText, 10, 10); // Add the content as text in the PDF
      pdf.save("generated.pdf");
    }
  };

  return (
    <div className="report-page">
      <section className="raport" id="service-report-pdf">
        <GridMenuHeader headerTitle="Raport" />
        <div className="container">
          <p>Typ raportu: {report?.reportType}</p>
          <div className="divider"></div>
          <p>Usługa: {service?.serviceType}</p>
          <div className="divider"></div>
          <p>Miasto: {report?.city}</p>
          <div className="divider"></div>
          <p>
            <span>Data od: {report?.beginDate.slice(0, 10)}</span>
            <span> Data do:{report?.endDate.slice(0, 10)}</span>
          </p>
          <div className="divider"></div>
          <p>Klient: {report?.city}</p>
          <div className="divider"></div>
          <p>Opis: {report?.description ?? "Brak"}</p>
          <div className="divider"></div>
          <p>Łączne koszty: {report?.amount}</p>
          <div className="divider"></div>
          <p>Cena: {service?.price}</p>
          <div className="divider"></div>
          <p>Bilans: {service?.price + report?.amount}</p>
          <div className="divider"></div>
          <p>Wygenerowano przez: {report?.author || "admin"}</p>
        </div>
      </section>
      <button onClick={generatePDF}>Wygeneruj pdf</button>
    </div>
  );
}

export default ServiceReport;
