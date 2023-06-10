import React from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Login from './pages/login.jsx';
import Menu from './pages/menu.jsx';
import ReportGeneration from './pages/reportGeneration.jsx';
import Reports, { loader as reportsLoader } from './pages/reports.jsx';
import Reservations, {loader as reservationsLoader} from './pages/reservations.jsx';
import ReservationsForm from './pages/reservationsForm.jsx';
import Workers, { loader as workersLoader } from './pages/workers.jsx';
import EditReservation from './pages/editReservation.jsx';
import Resources, {loader as resourcesLoader} from './pages/resources.jsx';

const router = createBrowserRouter([
  {
    path: '/',
    //element: <Root />,
    id: 'root',
    children: [
      {
        path: 'menu',
        element: <Menu />
      },
      {
        path: 'login',
        element: <Login />,
      },
      {
        id: 'reports',
        path: "raporty",
        element: <Reports />,
        loader: reportsLoader,
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
