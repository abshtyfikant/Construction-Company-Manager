import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './pages/login.jsx';
import Menu from './pages/menu.jsx';
import ReportGeneration from './pages/reportGeneration.jsx';
import Reports from './pages/reports.jsx';
import Reservations from './pages/reservations.jsx';
import ReservationsForm from './pages/reservationsForm.jsx';
import Workers from './pages/workers.jsx';

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/menu" element={<Menu />} />
          <Route path="/raporty" element={<Reports />} />
          <Route path="/generowanie-raportu" element={<ReportGeneration />} />
          <Route path="/rezerwacje" element={<Reservations />} />
          <Route path="/formularz-rejestracji" element={<ReservationsForm />} />
          <Route path="/stan-zasobow" element={<Menu />} />
          <Route path="/pracownicy" element={<Workers />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
