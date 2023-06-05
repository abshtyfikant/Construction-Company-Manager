import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown } from '@fortawesome/free-solid-svg-icons';
import GridMenuHeader from '../components/gridMenuHeader';
import reportsData from '../models/reportsData';

function Reports() {
  const [reports, setReports] = useState(reportsData);
  const [sortBy, setSortBy] = useState(''); // Kolumna, według której sortujemy
  const [sortOrder, setSortOrder] = useState(null); // Kierunek sortowania (asc/desc/null)
  const [isDefaultSort, setIsDefaultSort] = useState(true); // Informacja o domyślnym sortowaniu
  const [currentPage, setCurrentPage] = useState(1); // Aktualna strona
  const reportsPerPage = 10; // Liczba raportów na stronie

  // Funkcja sortująca raporty po kliknięciu w nagłówek kolumny
  const sortReports = (column) => {
    if (sortBy === column) {
      if (sortOrder === 'asc') {
        setSortOrder('desc');
      } else if (sortOrder === 'desc') {
        setSortBy('');
        setSortOrder(null);
        setIsDefaultSort(true);
      }
    } else {
      setSortBy(column);
      setSortOrder('asc');
      setIsDefaultSort(false);
    }
  };

  // Funkcja renderująca ikonki sortowania
  const renderSortIcons = (column) => {
    if (sortBy === column) {
      if (sortOrder === 'asc') {
        return (
          <div>
            <FontAwesomeIcon icon={faCaretUp} className="sort-icon active" />
            <FontAwesomeIcon icon={faCaretDown} className="sort-icon" />
          </div>
        );
      } else if (sortOrder === 'desc') {
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
    setSortBy('');
    setSortOrder(null);
    setIsDefaultSort(true);
  }, [currentPage]);

  // Funkcja renderująca raporty na aktualnej stronie
  const renderReports = () => {
    // Sortowanie raportów
    let sortedReports = [...reports];
    if (sortBy) {
      sortedReports.sort((a, b) => {
        if (a[sortBy] < b[sortBy]) {
          return sortOrder === 'asc' ? -1 : 1;
        }
        if (a[sortBy] > b[sortBy]) {
          return sortOrder === 'asc' ? 1 : -1;
        }
        return 0;
      });
    }

    // Paginacja raportów
    const indexOfLastReport = currentPage * reportsPerPage;
    const indexOfFirstReport = indexOfLastReport - reportsPerPage;
    const currentReports = sortedReports.slice(indexOfFirstReport, indexOfLastReport);

    // Renderowanie wierszy raportów
    return currentReports.map((report, index) => (
      <tr key={index}>
        <td>{report.rodzaj}</td>
        <td>{report.dataOd}</td>
        <td>{report.dataDo}</td>
        <td>{report.autor}</td>
        <td className='align-left'>{report.opis}</td>
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
  for (let i = 1; i <= pageNumbers; i++) {
    pagination.push(
      <li key={i} className={currentPage === i ? 'active' : ''}>
        <button onClick={() => handlePageChange(i)}>{i}</button>
      </li>
    );
  }

  return (
    <section className="reports">
      <GridMenuHeader headerTitle="Raporty" />
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th onClick={() => sortReports('rodzaj')}>
                <div>
                  Rodzaj raportu {renderSortIcons('rodzaj')}
                </div>
              </th>
              <th onClick={() => sortReports('dataOd')}>
                <div>
                  Data od {renderSortIcons('dataOd')}
                </div>
              </th>
              <th onClick={() => sortReports('dataDo')}>
                <div>
                  Data do {renderSortIcons('dataDo')}
                </div>
              </th>
              <th onClick={() => sortReports('autor')}>
                <div>
                  Autor {renderSortIcons('autor')}
                </div>
              </th>
              <th className='align-left'>Opis</th>
            </tr>
          </thead>
          <tbody>{renderReports()}</tbody>
        </table>
        <ul className="pagination">{pagination}</ul>
      </div>
    </section>
  );
}

export default Reports;
