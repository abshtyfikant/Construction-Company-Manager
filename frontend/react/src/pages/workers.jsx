import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown, faChevronLeft, faChevronRight, faTrash } from '@fortawesome/free-solid-svg-icons';
import GridMenuHeader from '../components/gridMenuHeader';
import { Link, useLoaderData, useNavigate, defer, json } from 'react-router-dom';
import classes from './styles/workers.module.css';

function Workers() {
  const token = localStorage.getItem('token');
  const navigate = useNavigate();
  const [workers, setWorkers] = useState([]);
  const [sortBy, setSortBy] = useState('');
  const [sortOrder, setSortOrder] = useState(null);
  const [isDefaultSort, setIsDefaultSort] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const workersPerPage = 10;
  const maxVisiblePages = 5;
  const ellipsis = '...';
  const [openDetails, setOpenDetails] = useState(false);

  const fetchWorkers = async () => {
    try {
      const response = await fetch('https://localhost:7098/api/Employee', {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token,
        },
      });

      if (response.ok) {
        const data = await response.json();
        setWorkers(data);
      } else {
        console.log('Błąd podczas pobierania danych z API:', response.status);
      }
    } catch (error) {
      console.log('Błąd podczas komunikacji z API:', error);
    }
  };

  useEffect(() => {
    fetchWorkers()
      .catch((error) => {
        console.log('Błąd podczas pobierania danych pracowników:', error);
      });
  }, []);

  const handleDelete = async (e, id) => {
    const response = await fetch('https://localhost:7098/api/Employee/' + id, {
        method: 'delete',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
    });

    if (!response.ok) {
        // return { isError: true, message: 'Could not fetch project.' };
        // throw new Response(JSON.stringify({ message: 'Could not fetch project.' }), {
        //   status: 500,
        // });
        throw json(
            { message: 'Could not fetch resource.' },
            {
                status: 500,
            }
        );
    } else {
        return navigate('/pracownicy');
    }
};

  const sortWorkers = (column) => {
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
  const renderWorkers = () => {
    // Sortowanie raportów
    let sortedWorkers = [...workers];
    if (sortBy) {
      sortedWorkers.sort((a, b) => {
        if (a[sortBy] < b[sortBy]) {
          return sortOrder === 'asc' ? -1 : 1;
        }
        if (a[sortBy] > b[sortBy]) {
          return sortOrder === 'asc' ? 1 : -1;
        }
        return 0;
      });
    }

    // Paginacja pracowników
    const indexOfLastWorker = currentPage * workersPerPage;
    const indexOfFirstWorker = indexOfLastWorker - workersPerPage;
    const currentWorkers = sortedWorkers.slice(indexOfFirstWorker, indexOfLastWorker);

    // Renderowanie wierszy pracowników
    return currentWorkers.map((worker, index) => (
      <>
        <tr key={index} onClick={() => setOpenDetails((prev) => !prev)}>
          <td>{worker.id}</td>
          <td>{worker.firstName}</td>
          <td>{worker.lastName}</td>
          <td>{worker.city}</td>
          <td>{worker.mainSpecialization}</td>
          <td className={classes.alignLeft}>{worker.ratePerHour}</td>
          <td className={classes.alignRight}>
            <button onClick={() => { navigate("/edytuj-pracownika", { state: { worker: worker } }) }}>Edytuj</button>
          </td>
        </tr>
      </>
    ));
  };

  const openWorkerDetails = () => {
  }

  // Funkcja zmieniająca aktualną stronę
  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

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

  const pageNumbers = Math.ceil(workers.length / workersPerPage);
  const pagination = [];

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
    <section className={classes.workers}>
      <GridMenuHeader headerTitle="Pracownicy" />
      <div className={classes.tableContainer}>
        <table className={classes.table}>
          <thead>
            <tr>
              <th onClick={() => sortWorkers('id')}>
                <div>
                  Id pracownika {renderSortIcons('id')}
                </div>
              </th>
              <th onClick={() => sortWorkers('firstName')}>
                <div>
                  Imię {renderSortIcons('firstName')}
                </div>
              </th>
              <th onClick={() => sortWorkers('lastName')}>
                <div>
                  Nazwisko {renderSortIcons('lastName')}
                </div>
              </th>
              <th onClick={() => sortWorkers('city')}>
                <div>
                  Miasto {renderSortIcons('city')}
                </div>
              </th>
              <th onClick={() => sortWorkers('workerSpecializationId')}>
                <div>
                  Specjalizacja {renderSortIcons('workerSpecializationId')}
                </div>
              </th>
              <th>
                <div className={classes.thAlignLeft}>
                  <p>Stawka godzinowa</p>
                  <div className={classes.thAlignRight}>
                    <Link to="/dodaj-pracownika">+ Dodaj pracownika</Link>
                  </div>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>{renderWorkers()}</tbody>
        </table>
        <ul className={classes.pagination}>{pagination}</ul>
      </div>
    </section>
  );
}

export default Workers;

async function loadWorkers() {
  const response = await fetch('<your_api_endpoint_here>');

  if (!response.ok) {
    throw json(
      { message: 'Could not fetch workers.' },
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
    workers: loadWorkers(),
  });
}
