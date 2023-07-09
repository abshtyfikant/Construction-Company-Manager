import React from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Login, { action as loginAuthAction } from './pages/login.jsx';
import Menu from './pages/menu.jsx';
import ReportGeneration from './pages/reportGeneration.jsx';
import Reports, { loader as reportsLoader } from './pages/reports.jsx';
import Reservations, { loader as reservationsLoader } from './pages/reservations.jsx';
import ReservationsForm from './pages/reservationsForm/reservationsForm.jsx';
import Workers, { loader as workersLoader } from './pages/workers.jsx';
import EditReservation from './pages/editReservation/editReservation.jsx';
import Resources, {loader as resourcesLoader} from './pages/resources/resources.jsx';
import AddWorker from './pages/addWorker.jsx';
import RemoveWorker from './pages/removeWorker.jsx';
import AddResource from './pages/addResource.jsx';
import { checkAuthLoader, tokenLoader } from './util/auth';
import Root from './pages/root.jsx';
import HomePage from './pages/home.jsx';
import Register, { action as registerAuthAction } from './pages/register.jsx';
import { action as logoutAction } from './pages/logout.jsx'
import ReportDetails, {loader as reportDetailsLoader} from './pages/reportDetails.jsx';
import AddSpecialization from './pages/addSpecialization.jsx'
import EditWorker from './pages/editWorker.jsx';
import EditResource from './pages/editResource.jsx';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    id: 'root',
    loader: tokenLoader,
    children: [
      {
        index: true,
        element: <HomePage />,
      },
      {
        path: 'menu',
        element: <Menu />,
      },
      {
        path: 'login',
        element: <Login />,
        action: loginAuthAction,
      },
      {
        path: 'zarejestruj-sie',
        element: <Register />,
        action: registerAuthAction,
      },
      {
        path: 'logout',
        action: logoutAction,
      },
      {
        id: 'reports',
        path: "raporty",
        element: <Reports />,
        loader: reportsLoader,
        children: [
          {
            path: ':reportId',
            id: 'report-details',
            loader: reportDetailsLoader,
            children: [
              {
                index: true,
                element: <ReportDetails />,
              },
            ]
          },
        ],
      },
      {
        path: "generowanie-raportu",
        element: <ReportGeneration />,
      },
      {
        path: "rezerwacje",
        element: <Reservations />,
        loader: reservationsLoader,
      },
      {
        path: "formularz-rezerwacji",
        element: <ReservationsForm />,
      },
      {
        path: "pracownicy",
        element: <Workers />,
        loader: workersLoader,
      },
      {
        path: "edytuj-rezerwacje",
        element: <EditReservation />,
      },
      {
        path: "stan-zasobow",
        element: <Resources />,
        loader: resourcesLoader,
      },
      {
        path: "dodaj-pracownika",
        element: <AddWorker />,
      },
      {
        path: "usun-pracownika",
        element: <RemoveWorker />,
      },
      {
        path: "edytuj-pracownika",
        element: <EditWorker />,
      },
      {
        path: "dodaj-zasob",
        element: <AddResource />,
      },
      {
        path: "edytuj-zasob",
        element: <EditResource />,
      },
      {
        path: "dodaj-specjalizacje",
        element: <AddSpecialization />,
      },
    ],
  },
]);

function App() {
  return (
    <div className="App">
      <RouterProvider router={router} />
    </div>
  );
}

export default App;
