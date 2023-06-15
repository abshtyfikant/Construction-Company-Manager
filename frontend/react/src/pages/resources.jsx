import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown, faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import GridMenuHeader from '../components/gridMenuHeader';
import resourcesData from '../models/resourcesData';
import { Link, useLoaderData, useNavigate, defer, json } from 'react-router-dom';
import classes from './styles/resources.module.css';

function Resources() {
  const navigate = useNavigate();
  //odkomentować po połączeniu z backendem
  //const { resources } = useLoaderData();

  //usunac po połączeniu z backendem
  const [resources, setResources] = useState(resourcesData);
  const [sortBy, setSortBy] = useState(''); // Kolumna, według której sortujemy
  const [sortOrder, setSortOrder] = useState(null); // Kierunek sortowania (asc/desc/null)
  const [isDefaultSort, setIsDefaultSort] = useState(true); // Informacja o domyślnym sortowaniu
  const [currentPage, setCurrentPage] = useState(1); // Aktualna strona
  const resourcesPerPage = 10; // Liczba raportów na stronie
  const maxVisiblePages = 5; // Maksymalna liczba widocznych stron paginacji
  const ellipsis = '...'; // Symbol kropek
  const [openDetails, setOpenDetails] = useState(false);

  // Funkcja sortująca zasoby po kliknięciu w nagłówek kolumny
  const sortResources = (column) => {
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
  const renderResources = () => {
    // Sortowanie raportów
    let sortedResources = [...resources];
    if (sortBy) {
      sortedResources.sort((a, b) => {
        if (a[sortBy] < b[sortBy]) {
          return sortOrder === 'asc' ? -1 : 1;
        }
        if (a[sortBy] > b[sortBy]) {
          return sortOrder === 'asc' ? 1 : -1;
        }
        return 0;
      });
    }

    // Paginacja zasobów
    const indexOfLastResource = currentPage * resourcesPerPage;
    const indexOfFirstResource = indexOfLastResource - resourcesPerPage;
    const currentResources = sortedResources.slice(indexOfFirstResource, indexOfLastResource);

    // Renderowanie wierszy zasobów
    return currentResources.map((resource, index) => (
      <>
        <tr key={index} onClick={() => setOpenDetails((prev) => !prev)}>
          <td>{resource.id}</td>
          <td>{resource.resourceName}</td>
          <td className={classes.alignLeft}>{resource.quantity}</td>
          <td className={classes.alignRight}>
            <FontAwesomeIcon icon={faCaretDown} className={classes.sortIcon} />
          </td>
        </tr>
        {openDetails ? (
          <tr className={classes.dropdownDetails}>
            <td colSpan={3}>
              <p>Klient:</p>
              <p>Zespół wykonawczy:</p>
            </td>
            <td colSpan={3}>
              <p>Przydział zasobów:</p>
              <p>Materiały:</p>
            </td>
            <td colSpan={3}>
              <p>Koszt materiałów:</p>
              <p>Koszt pracowników:</p>
              <p
                className={classes.editResource}
                onClick={() => { navigate("/edytuj-rezerwacje", { state: { resource: resource } }) }}
              >
                modyfikuj rezerwację
              </p>
              <p>generuj raport</p>
              <p>+ Dodaj komentarz</p>
            </td>
          </tr>
        )
          : null}
      </>
    ));
  };

  const openResourceDetails = () => {

  }

  // Funkcja zmieniająca aktualną stronę
  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  // Generowanie numerów stron do paginacji
  const pageNumbers = Math.ceil(resources.length / resourcesPerPage);
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
    <section className={classes.resources}>
      <GridMenuHeader headerTitle="Stan zasobów" />
      <div className={classes.tableContainer}>
        <table className={classes.table}>
          <thead>
            <tr>
              <th onClick={() => sortResources('id')}>
                <div>
                  Id zasobu {renderSortIcons('id')}
                </div>
              </th>
              <th onClick={() => sortResources('resourceName')}>
                <div>
                  Nazwa zasobu {renderSortIcons('resourceName')}
                </div>
              </th>
              <th>
                <div className={classes.thAlignLeft}>
                  <p>Ilość</p>
                  <div className={classes.thAlignRight}>
                    <Link to="/dodaj-zasob">+ Dodaj zasób</Link>
                  </div>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>{renderResources()}</tbody>
        </table>
        <ul className={classes.pagination}>{pagination}</ul>
      </div>
    </section>
  );
}

export default Resources;

async function loadRespurces() {
  const response = await fetch('');

  if (!response.ok) {
    // return { isError: true, message: 'Could not fetch events.' };
    // throw new Response(JSON.stringify({ message: 'Could not fetch events.' }), {
    //   status: 500,
    // });
    throw json(
      { message: 'Could not fetch resources.' },
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
    resources: loadRespurces(),
  });
}
