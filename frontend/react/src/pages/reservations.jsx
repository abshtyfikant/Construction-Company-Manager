import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown, faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import GridMenuHeader from '../components/gridMenuHeader';
import { Link, useLoaderData, useNavigate, defer, json } from 'react-router-dom';
import classes from './styles/reservations.module.css';
import Comments from '../components/comments';
import Loading from '../components/loading';

function Accordion({ reservation, index, handleOpenComments }) {
  const navigate = useNavigate();
  const [openDetails, setOpenDetails] = useState(false);
  const [sumMaterials, setSumMaterials] = useState();

  const calcSumMaterials = () => {
    let tmpSumMaterials = 0;
    tmpSumMaterials += reservation?.materials?.map((material) => {
      console.log(tmpSumMaterials)
      return (
        parseFloat(material.price) * parseFloat(material.quantity)
      );
    });
    setSumMaterials(tmpSumMaterials)
  }

  return (
    <>
      <tr key={index} onClick={() => {setOpenDetails((prev) => !prev); calcSumMaterials();}}>
        <td>{reservation.id}</td>
        <td>{reservation.serviceType}</td>
        <td>{reservation.beginDate.slice(0, 10)}</td>
        <td>{reservation.endDate.slice(0, 10)}</td>
        <td>{reservation.city}</td>
        <td>{reservation.serviceStatus}</td>
        <td className={classes.alignLeft}>{reservation.paymentStatus}</td>
        <td className={classes.alignRight}>
          <FontAwesomeIcon icon={faCaretDown} className={classes.sortIcon} />
        </td>
      </tr>
      {openDetails ? (
        <tr className={classes.dropdownDetails}>
          <td colSpan={3}>
            <p>Klient: {reservation.client?.firstName} {reservation.client?.lastName}</p>
            <p>Zespół wykonawczy:</p>
            {reservation.workers && reservation.workers.map((worker) => {
              return (
                <p key={worker.id}>{worker.name} {worker.surname}</p>
              );
            })}
          </td>
          <td colSpan={3}>
            <p>Przydział zasobów:</p>
            <p>Nazwa | Ilość | Jednostka </p>
            {reservation.resources && reservation.resources.map((resource) => {
              return (
                <p key={resource.id}>{resource.name} {resource.quantity} {resource.unit}</p>
              );
            })}
            <p>Materiały:</p>
            <p>Nazwa | Cena | Ilość | Jednostka </p>
            {reservation.materials && reservation.materials.map((material) => {
              return (
                <p key={material.id}>{material.name} | {material.price} | {material.quantity} | {material.unit}</p>
              );
            })}
          </td>
          <td colSpan={3}>
            <p>Koszt materiałów: {sumMaterials}</p>
            <p>Koszt pracowników:</p>
            <p
              className={classes.actions}
              onClick={() => { navigate("/edytuj-rezerwacje", { state: { reservation: reservation } }) }}
            >
              + Modyfikuj rezerwację
            </p>
            <p className={classes.actions}>+ Generuj raport</p>
            <p onClick={() => handleOpenComments(true)} className={classes.actions}>+ Dodaj komentarz</p>
          </td>
        </tr>
      )
        : null
      }
    </>
  )
}


function Reservations() {
  const navigate = useNavigate();
  const token = localStorage.getItem('token');
  const [reservations, setReservations] = useState([]); // Dane raportów z API
  const [sortBy, setSortBy] = useState(''); // Kolumna, według której sortujemy
  const [sortOrder, setSortOrder] = useState(null); // Kierunek sortowania (asc/desc/null)
  const [isDefaultSort, setIsDefaultSort] = useState(true); // Informacja o domyślnym sortowaniu
  const [currentPage, setCurrentPage] = useState(1); // Aktualna strona
  const reservationsPerPage = 10; // Liczba raportów na stronie
  const maxVisiblePages = 5; // Maksymalna liczba widocznych stron paginacji
  const ellipsis = '...'; // Symbol kropek const [openComments, setOpenComments] = useState(false);
  const [openComments, setOpenComments] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  // Funkcja pobierająca dane raportów z API
  async function fetchReservations() {
    let tmpReservations = [];
    try {
      const response = await fetch('https://localhost:7098/api/Service', {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token,
        },
      });

      if (response.ok) {
        const data = await response.json();
        tmpReservations = data;
      } else {
        console.log('Błąd podczas pobierania danych z API:', response.status);
      }
    } catch (error) {
      console.log('Błąd podczas komunikacji z API:', error);
    }

    tmpReservations.map(async (reservation) => {
      try {
        const response = await fetch('https://localhost:7098/api/Client/' + reservation.clientId, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
          },
        });

        if (response.ok) {
          const data = await response.json();
          reservation.client = data;
        } else {
          console.log('Błąd podczas pobierania danych z API:', response.status);
        }
      } catch (error) {
        console.log('Błąd podczas komunikacji z API:', error);
      }

      try {
        const response = await fetch('https://localhost:7098/api/ResourceAllocation/Service/' + reservation.id, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
          },
        });

        if (response.ok) {
          const data = await response.json();
          reservation.resources = data;
        } else {
          console.log('Błąd podczas pobierania danych z API:', response.status);
        }
      } catch (error) {
        console.log('Błąd podczas komunikacji z API:', error);
      }

      try {
        const response = await fetch('https://localhost:7098/api/Assignment/GetServiceAssignments/' + reservation.id, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
          },
        });

        if (response.ok) {
          const data = await response.json();
          reservation.workers = data;
        } else {
          console.log('Błąd podczas pobierania danych z API:', response.status);
        }
      } catch (error) {
        console.log('Błąd podczas komunikacji z API:', error);
      }

      try {
        const response = await fetch('https://localhost:7098/api/Material/GetByService/' + reservation.id, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
          },
        });

        if (response.ok) {
          const data = await response.json();
          reservation.materials = data;
        } else {
          console.log('Błąd podczas pobierania danych z API:', response.status);
        }
      } catch (error) {
        console.log('Błąd podczas komunikacji z API:', error);
      }
      setIsLoading(false);
    })


    console.log(tmpReservations);
    setReservations(tmpReservations);
  }

  // Efekt, który pobiera dane raportów z API przy ładowaniu komponentu
  useEffect(() => {
    fetchReservations();
  }, []);

  // Funkcja sortująca rezerwacje po kliknięciu w nagłówek kolumny
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

  const handleOpenComments = (open) => {
    setOpenComments(open);
  };

  // Funkcja renderująca rezerwacje na aktualnej stronie
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

    // Paginacja rezerwacji
    const indexOfLastReservation = currentPage * reservationsPerPage;
    const indexOfFirstReservation = indexOfLastReservation - reservationsPerPage;
    const currentReservations = sortedReservations.slice(indexOfFirstReservation, indexOfLastReservation);

    // Renderowanie wierszy rezerwacji
    return currentReservations.map((reservation, index) => (
      <>
        {console.log(currentReservations)}
        <Accordion reservation={reservation} index={index} handleOpenComments={handleOpenComments} />
        <Comments isOpen={openComments} serviceId={reservation.id} />
      </>
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
    <Loading isVisible={isLoading}/>
      <span
        className={classes.backdrop}
        style={{ display: openComments ? 'block' : 'none' }}
        onClick={() => setOpenComments(false)} />
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
              <th onClick={() => sortReservations('serviceType')}>
                <div>
                  Typ usługi {renderSortIcons('serviceType')}
                </div>
              </th>
              <th onClick={() => sortReservations('beginDate')}>
                <div>
                  Data od {renderSortIcons('beginDate')}
                </div>
              </th>
              <th onClick={() => sortReservations('endDate')}>
                <div>
                  Data do {renderSortIcons('endDate')}
                </div>
              </th>
              <th onClick={() => sortReservations('city')}>
                <div>
                  Miasto {renderSortIcons('city')}
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

async function loadReservations() {
  const response = await fetch('');

  if (!response.ok) {
    // return { isError: true, message: 'Could not fetch events.' };
    // throw new Response(JSON.stringify({ message: 'Could not fetch events.' }), {
    //   status: 500,
    // });
    throw json(
      { message: 'Could not fetch reservations.' },
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
    reservations: loadReservations(),
  });
}
