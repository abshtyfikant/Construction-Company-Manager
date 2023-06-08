import GridMenuHeader from '../components/gridMenuHeader';
import classes from './styles/reportGeneration.module.css';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

const cities = [
  'Warszawa', 'Katowice', 'Kraków'
];

const types = [
  'typ1', 'typ2', 'typ3'
];

const workers = [
  'Kowalski', 'Nowak', 'Marek'
];

function ReportGeneration() {
  const [description, setDescription] = React.useState('');
  const navigate = useNavigate();
  const [city, setCity] = React.useState('');
  const [startDate, setStartDate] = React.useState('');
  const [endDate, setEndDate] = React.useState('');
  const [reportType, setReportType] = React.useState('');
  const [worker, setWorker] = React.useState('');

  return (
    <div className={classes.container}>
      <GridMenuHeader headerTitle="Generowanie raportu" />
      <form className={classes.formContainer}>
        <div className={classes.formContent}>
          <div className={classes.splitSection}>
            <label for='report-type' className={classes.label}>Rodzaj raportu:</label>
            <label for='worker' className={classes.label}>Wybierz pracownika:</label>
            <select
              className={classes.formInput}
              id='report-type'
              onChange={(e) => setReportType(e.target.value)}
              reqiured
            >
              <option value=''>Wybierz z listy</option>
              {types.map((type) => (
                <option key={type} value={type}>{type}</option>
              ))}
            </select>
            <select
              className={classes.formInput}
              id='worker'
              onChange={(e) => setWorker(e.target.value)}
              required
            >
              <option value=''>Wybierz z listy</option>
              {workers.map((worker) => (
                <option key={worker} value={worker}>{worker}</option>
              ))}
            </select>
          </div>
          <label for='start-date' className={classes.label}>Data od:</label>
          <input
            className={classes.formInput}
            id='start-date'
            type='date'
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            required
          />
          <label for='end-date' className={classes.label}>Data do:</label>
          <input
            className={classes.formInput}
            id='end-date'
            type='date'
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
            required
          />
          <label for='city' className={classes.label}>Miasto (opcj.):</label>
          <select
            className={classes.formInput}
            id='city'
            onChange={(e) => setCity(e.target.value)}
          >
            <option value=''>Wybierz z listy</option>
            {cities.map((city) => (
              <option key={city} value={city}>{city}</option>
            ))}
          </select>
          <label for='description' className={classes.label}>Opis (opcj.):</label>
          <input
            className={classes.formInput}
            id='description'
            type='text'
            value={description}
            placeholder='Dodaj opis raportu...'
            onChange={(e) => setDescription(e.target.value)}
          />
        </div>
        <button type='submit' className={classes.submitBtn}>Generuj raport</button>
      </form>
    </div>
  );
}

export default ReportGeneration;
