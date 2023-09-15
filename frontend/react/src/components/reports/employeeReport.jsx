import "../../css/report-details.css";
import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import GridMenuHeader from "../gridMenuHeader";
import jsPDF from "jspdf";

function EmployeeReport() {
  const { reportId, startDate, endDate } = useParams();
  const [report, setReport] = useState(null); // Dane raportu z API
  const [employee, setEmployee] = useState(null);

  // Funkcja pobierająca dane raportu z API
  async function fetchReportDetails(id) {
    const token = localStorage.getItem("token");
    const response = await fetch(
      `https://localhost:7098/api/Report/${Number(id)}`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    );
    let data;
    if (response.ok) {
      data = await response.json();
      setReport(data);
    } else {
      alert(
        "Wystąpił błąd podczas ładowania danych. Spróbuj ponownie za chwilę."
      );
      return;
    }

    const response2 = await fetch(
      `https://localhost:7098/api/Employee/${data.employeeId}`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    );

    if (response2.ok) {
      const data2 = await response2.json();
      setEmployee(data2);
    } else {
      alert(
        "Wystąpił błąd podczas ładowania danych. Spróbuj ponownie za chwilę."
      );
      return;
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
    const element = document.getElementById("employee-report-pdf");

    if (element) {
      const pdf = new jsPDF();
      pdf.text(element.innerText, 10, 10); // Add the content as text in the PDF
      pdf.save(`report-${reportId}.pdf`);
    }
  };

  return (
    <div className="report-page">
      <section className="raport" id="employee-report-pdf">
        <GridMenuHeader headerTitle="Raport" />
        <div className="container">
          <p>Typ raportu: {report.reportType}</p>
          <div className="divider"></div>
          <p>Pracownik: {employee?.firstName + " " + employee?.lastName}</p>
          <div className="divider"></div>
          <p>Data od: {report.beginDate.slice(0, 10)}</p>
          <div className="divider"></div>
          <p>Data do: {report.endDate.slice(0, 10)}</p>
          <div className="divider"></div>
          <p>Miasto: {report.city}</p>
          <div className="divider"></div>
          <p>Opis: {report.description}</p>
          <div className="divider"></div>
          <p>Łączna kwota: {report.amount}</p>
          <div className="divider"></div>
          <p>Wygenerowano przez: {report.author || "admin"}</p>
        </div>
      </section>
      <button onClick={generatePDF}>Wygeneruj pdf</button>
    </div>
  );
}

export default EmployeeReport;
