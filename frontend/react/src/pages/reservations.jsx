import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown, faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import GridMenuHeader from '../components/gridMenuHeader';
import reservationsData from '../models/reservationsData';
import { Link } from 'react-router-dom';
import classes from './styles/reservations.module.css';

function Reservations() {
  const [reservations, setReservations] = useState(reservationsData);
  const [sortBy, setSortBy] = useState(''); // Kolumna, według której sortujemy
  const [sortOrder, setSortOrder] = useState(null); // Kierunek sortowania (asc/desc/null)
  const [isDefaultSort, setIsDefaultSort] = useState(true); // Informacja o domyślnym sortowaniu
  const [currentPage, setCurrentPage] = useState(1); // Aktualna strona
  const reservationsPerPage = 10; // Liczba raportów na stronie
  const maxVisiblePages = 5; // Maksymalna liczba widocznych stron paginacji
  const ellipsis = '...'; // Symbol kropek

  // Funkcja sortująca raporty po kliknięciu w nagłówek kolumny
  const sortReservations = (column) => {
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
            <FontAwesomeIcon icon={faCaretUp} className={`${classes.sortIcon} ${classes.sortIconActive}`} />
            <FontAwesomeIcon icon={faCaretDown} className={classes.sortIcon} />
          </div>
        );
      } else if (sortOrder === 'desc') {
        return (
          <div>
            <FontAwesomeIcon icon={faCaretUp} className={classes.sortIcon} />
            <FontAwesomeIcon icon={faCaretDown} className={`${classes.sortIcon} ${classes.sortIconActive}`} />
          </div>
        );
      }
    }
    return (
      <div>
        <FontAwesomeIcon icon={faCaretUp} className={classes.sortIcon} />
        <FontAwesomeIcon icon={faCaretDown} className={classes.sortIcon} />
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
  const renderReservations = () => {
    // Sortowanie raportów
    let sortedReservations = [...reservations];
    if (sortBy) {
      sortedReservations.sort((a, b) => {
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
    const indexOfLastReservation = currentPage * reservationsPerPage;
    const indexOfFirstReservation = indexOfLastReservation - reservationsPerPage;
    const currentReservations = sortedReservations.slice(indexOfFirstReservation, indexOfLastReservation);

    // Renderowanie wierszy raportów
    return currentReservations.map((report, index) => (
      <tr key={index}>
        <td>{report.id}</td>
        <td>{report.typ}</td>
        <td>{report.dataOd}</td>
        <td>{report.dataDo}</td>
        <td>{report.miasto}</td>
        <td>{report.statusUslugi}</td>
        <td className={classes.alignLeft}>{report.statusPlatnosci}</td>
      </tr>
    ));
  };

  // Funkcja zmieniająca aktualną stronę
  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  // Generowanie numerów stron do paginacji
  const pageNumbers = Math.ceil(reservations.length / reservationsPerPage);
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
      <li key={pageNumber} className={currentPage === pageNumber ? `${classes.active}` : `${''}`}>
        <button onClick={() => handlePageChange(pageNumber)}>{label}</button>
      </li>
    );
  };

  pagination.push(
    <li key="prev" className={currentPage === 1 ? `${classes.disabled}` : `${''}`}>
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
          <li key="left-ellipsis" className={classes.disabled}>
            <button disabled>{ellipsis}</button>
          </li>
        );
        leftEllipsis = true;
      } else if (
        i > currentPage + Math.floor(maxVisiblePages / 2) &&
        !rightEllipsis
      ) {
        pagination.push(
          <li key="right-ellipsis" className={classes.disabled}>
            <button disabled>{ellipsis}</button>
          </li>
        );
        rightEllipsis = true;
      }
    }
  }

  pagination.push(
    <li key="next" className={currentPage === pageNumbers ? `${classes.disabled}` : `${''}`}>
      <button onClick={handleNextPage}>
        <FontAwesomeIcon icon={faChevronRight} />
      </button>
    </li>
  );

  return (
    <section className={classes.reservations}>
      <GridMenuHeader headerTitle="Rezerwacje" />
      <div className={classes.tableContainer}>
        <table className={classes.table}>
          <thead>
            <tr>
              <th onClick={() => sortReservations('id')}>
                <div>
                  Id usługi {renderSortIcons('id')}
                </div>
              </th>
              <th onClick={() => sortReservations('typ')}>
                <div>
                  Typ usługi {renderSortIcons('typ')}
                </div>
              </th>
              <th onClick={() => sortReservations('dataOd')}>
                <div>
                  Data od {renderSortIcons('dataOd')}
                </div>
              </th>
              <th onClick={() => sortReservations('dataDo')}>
                <div>
                  Data do {renderSortIcons('dataDo')}
                </div>
              </th>
              <th onClick={() => sortReservations('miasto')}>
                <div>
                  Miasto {renderSortIcons('miasto')}
                </div>
              </th>
              <th>
                <div className={classes.thAlignLeft}>
                  <p>Status usługi</p>
                </div>
              </th>
              <th>
                <div className={classes.thAlignLeft}>
                  <p>Status płatności</p>
                  <div className={classes.thAlignRight}>
                  <Link to="/formularz-rezerwacji">+ Nowa rezerwacja</Link>
                  </div>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>{renderReservations()}</tbody>
        </table>
        <ul className={classes.pagination}>{pagination}</ul>
      </div>
    </section>
  );
}

export default Reservations;
