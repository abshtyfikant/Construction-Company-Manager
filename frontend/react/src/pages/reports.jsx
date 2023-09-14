import React, { useState, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCaretUp,
  faCaretDown,
  faChevronLeft,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons";
import GridMenuHeader from "../components/gridMenuHeader";
import { Link, defer, json } from "react-router-dom";

function Reports() {
  const token = localStorage.getItem("token");
  const [reports, setReports] = useState([]); // Dane raportów z API
  const [sortBy, setSortBy] = useState(""); // Kolumna, według której sortujemy
  const [sortOrder, setSortOrder] = useState(null); // Kierunek sortowania (asc/desc/null)
  const [isDefaultSort, setIsDefaultSort] = useState(true); // Informacja o domyślnym sortowaniu
  const [currentPage, setCurrentPage] = useState(1); // Aktualna strona
  const reportsPerPage = 10; // Liczba raportów na stronie
  const maxVisiblePages = 5; // Maksymalna liczba widocznych stron paginacji
  const ellipsis = "..."; // Symbol kropek

  // Funkcja pobierająca dane raportów z API
  async function fetchReports() {
    try {
      const response = await fetch("https://localhost:7098/api/Report", {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      });

      if (response.ok) {
        const data = await response.json();
        setReports(data);
      }
    } catch (error) {}
  }
  // Efekt, który pobiera dane raportów z API przy ładowaniu komponentu
  useEffect(() => {
    fetchReports();
  }, []);

  // Funkcja sortująca raporty po kliknięciu w nagłówek kolumny
  const sortReports = (column) => {
    if (sortBy === column) {
      if (sortOrder === "asc") {
        setSortOrder("desc");
      } else if (sortOrder === "desc") {
        setSortBy("");
        setSortOrder(null);
        setIsDefaultSort(true);
      }
    } else {
      setSortBy(column);
      setSortOrder("asc");
      setIsDefaultSort(false);
    }
  };

  // Funkcja renderująca ikonki sortowania
  const renderSortIcons = (column) => {
    if (sortBy === column) {
      if (sortOrder === "asc") {
        return (
          <div>
            <FontAwesomeIcon icon={faCaretUp} className="sort-icon active" />
            <FontAwesomeIcon icon={faCaretDown} className="sort-icon" />
          </div>
        );
      } else if (sortOrder === "desc") {
        return (
          <div>
            <FontAwesomeIcon icon={faCaretUp} className="sort-icon" />
            <FontAwesomeIcon icon={faCaretDown} className="sort-icon active" />
          </div>
        );
      }
    }
    return (
      <div>
        <FontAwesomeIcon icon={faCaretUp} className="sort-icon" />
        <FontAwesomeIcon icon={faCaretDown} className="sort-icon" />
      </div>
    );
  };

  // Efekt, który resetuje sortowanie po zmianie strony
  useEffect(() => {
    setSortBy("");
    setSortOrder(null);
    setIsDefaultSort(true);
  }, [currentPage]);

  const renderButton = (report) => {
    console.log(report);
    switch (report.reportType) {
      case "raport z usługi":
        return <Link to={`/raport-z-uslugi/${report.id}`}>Podgląd</Link>;
      case "raport o kosztach":
        return <Link to={`/raport-o-kosztach/${report.id}`}>Podgląd</Link>;
      case "raport o zarobkach firmy":
        return <Link to={`/raport-o-zarobkach/${report.id}`}>Podgląd</Link>;
      case "raport o zarobkach pracownika":
        return (
          <Link
            to={`/raport-o-pracowniku/${report.id}/${report.beginDate.slice(
              0,
              10
            )}/${report.endDate.slice(0,10)}`}
          >
            Podgląd
          </Link>
        );
    }
  };
  console.log(reports);
  // Funkcja renderująca raporty na aktualnej stronie
  const renderReports = () => {
    // Sortowanie raportów
    let sortedReports = [...reports];
    if (sortBy) {
      sortedReports.sort((a, b) => {
        if (a[sortBy] < b[sortBy]) {
          return sortOrder === "asc" ? -1 : 1;
        }
        if (a[sortBy] > b[sortBy]) {
          return sortOrder === "asc" ? 1 : -1;
        }
        return 0;
      });
    }

    // Paginacja raportów
    const indexOfLastReport = currentPage * reportsPerPage;
    const indexOfFirstReport = indexOfLastReport - reportsPerPage;
    const currentReports = sortedReports.slice(
      indexOfFirstReport,
      indexOfLastReport
    );

    // Renderowanie wierszy raportów
    return currentReports.map((report, index) => (
      <tr key={index}>
        <td>{report.reportType}</td>
        <td>{report.beginDate}</td>
        <td>{report.endDate}</td>
        <td>{report.author}</td>
        <td className="align-left">
          {report.description.length > 30
            ? `${report.description.substring(0, 30)}...`
            : report.description}
        </td>

        <td className="raport-btn">{renderButton(report)}</td>
      </tr>
    ));
  };

  // Funkcja zmieniająca aktualną stronę
  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  // Generowanie numerów stron do paginacji
  const pageNumbers = Math.ceil(reports.length / reportsPerPage);
  const pagination = [];

  const handlePrevPage = () => {
    if (currentPage > 1) {
      setCurrentPage(currentPage - 1);
    }
  };

  const handleNextPage = () => {
    if (currentPage < pageNumbers) {
      setCurrentPage(currentPage + 1);
    }
  };

  const renderPaginationItem = (pageNumber, label) => {
    return (
      <li
        key={pageNumber}
        className={currentPage === pageNumber ? "active" : ""}
      >
        <button onClick={() => handlePageChange(pageNumber)}>{label}</button>
      </li>
    );
  };

  pagination.push(
    <li key="prev" className={currentPage === 1 ? "disabled" : ""}>
      <button onClick={handlePrevPage}>
        <FontAwesomeIcon icon={faChevronLeft} />
      </button>
    </li>
  );

  if (pageNumbers <= maxVisiblePages) {
    for (let i = 1; i <= pageNumbers; i++) {
      pagination.push(renderPaginationItem(i, i));
    }
  } else {
    let leftEllipsis = false;
    let rightEllipsis = false;

    for (let i = 1; i <= pageNumbers; i++) {
      if (
        i === 1 ||
        i === pageNumbers ||
        (i >= currentPage - Math.floor(maxVisiblePages / 2) &&
          i <= currentPage + Math.floor(maxVisiblePages / 2))
      ) {
        pagination.push(renderPaginationItem(i, i));
      } else if (
        i < currentPage - Math.floor(maxVisiblePages / 2) &&
        !leftEllipsis
      ) {
        pagination.push(
          <li key="left-ellipsis" className="disabled">
            <button disabled>{ellipsis}</button>
          </li>
        );
        leftEllipsis = true;
      } else if (
        i > currentPage + Math.floor(maxVisiblePages / 2) &&
        !rightEllipsis
      ) {
        pagination.push(
          <li key="right-ellipsis" className="disabled">
            <button disabled>{ellipsis}</button>
          </li>
        );
        rightEllipsis = true;
      }
    }
  }

  pagination.push(
    <li key="next" className={currentPage === pageNumbers ? "disabled" : ""}>
      <button onClick={handleNextPage}>
        <FontAwesomeIcon icon={faChevronRight} />
      </button>
    </li>
  );

  return (
    <section className="reports">
      <GridMenuHeader headerTitle="Raporty" />
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th onClick={() => sortReports("reportType")}>
                <div>Rodzaj raportu {renderSortIcons("reportType")}</div>
              </th>
              <th onClick={() => sortReports("beginDate")}>
                <div>Data od {renderSortIcons("beginDate")}</div>
              </th>
              <th onClick={() => sortReports("endDate")}>
                <div>Data do {renderSortIcons("endDate")}</div>
              </th>
              <th onClick={() => sortReports("author")}>
                <div>Autor {renderSortIcons("author")}</div>
              </th>
              <th>
                <div className="th-align-left">
                  <p>Opis</p>
                  <div className="th-align-right">
                    <Link to="/generowanie-raportu">
                      + Wygeneruj nowy raport
                    </Link>
                  </div>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>{Object.keys(reports).length !== 0 && renderReports()}</tbody>
        </table>
        <ul className="pagination">{pagination}</ul>
      </div>
    </section>
  );
}

export default Reports;

async function loadReports() {
  const token = localStorage.getItem("token");
  const response = await fetch("https://localhost:7098/api/Report", {
    method: "get",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
  });

  if (!response.ok) {
    // return { isError: true, message: 'Could not fetch events.' };
    // throw new Response(JSON.stringify({ message: 'Could not fetch events.' }), {
    //   status: 500,
    // });
    throw json(
      { message: "Could not fetch reports." },
      {
        status: 500,
      }
    );
  } else {
    const resData = await response.json();
    return resData;
  }
}

export async function loader() {
  return defer({
    reports: loadReports(),
  });
}
