import GridMenuHeader from '../components/gridMenuHeader';
import classes from './styles/reportGeneration.module.css';
import * as React from 'react';
import { useNavigate, json } from 'react-router-dom';

const cities = [
  'Warszawa', 'Katowice', 'Kraków'
];

const types = [
  'raport o zarobkach firmy', 'raport o zarobkach pracownika', 'raport o kosztach', 'raport z usługi'
];

const workers = [
  'Kowalski', 'Nowak', 'Marek'
];

function ReportGeneration() {
  const token = localStorage.getItem('token');
  const userId = localStorage.getItem('userId');
  const [description, setDescription] = React.useState('');
  const navigate = useNavigate();
  const [city, setCity] = React.useState('');
  const [startDate, setStartDate] = React.useState('');
  const [endDate, setEndDate] = React.useState('');
  const [reportType, setReportType] = React.useState('');
  const [worker, setWorker] = React.useState('');
  const [fetchedWorkers, setFetchedWorkers] = React.useState('');
  const [fetchedServices, setFetchedServices] = React.useState('');

  const fetchData = React.useCallback(async () => {
    try {
      const response = await fetch('https://localhost:7098/api/Employee', {
        method: 'get',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token,
        },
      });

      if (!response.ok) {
        throw new Error('Something went wrong!');
      }

      const data = await response.json();
      setFetchedWorkers(data);

    } catch (error) {
      //setError("Something went wrong, try again.");
    }

    try {
      const response = await fetch('https://localhost:7098/api/Service', {
        method: 'get',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token,
        },
      });
      if (!response.ok) {
        throw new Error('Something went wrong!');
      }

      const data = await response.json();
      setFetchedServices(data);

    } catch (error) {
      //setError("Something went wrong, try again.");
    }
  }, []);

  React.useEffect(() => {
    fetchData();
  }, [fetchData]);


  const renderSelect = () => {
    switch (reportType) {
      case 'raport o zarobkach firmy':
        return (
          <select
            className={classes.formInput}
            id='worker'
            onChange={(e) => setWorker(e.target.value)}
            disabled
          >
            <option value=''>Wybierz z listy</option>
          </select>
        );

      case 'raport o zarobkach pracownika':
        return (
          <select
            className={classes.formInput}
            id='worker'
            onChange={(e) => setWorker(e.target.value)}
            required
          >
            <option value=''>Wybierz z listy</option>
            {fetchedWorkers && fetchedWorkers.map((worker) => (
              <option key={worker} value={worker}>{worker}</option>
            ))}
          </select>
        );

      case 'raport o kosztach':
        return (
          <select
            className={classes.formInput}
            id='worker'
            onChange={(e) => setWorker(e.target.value)}
            disabled
          >
            <option value=''>Wybierz z listy</option>
          </select>
        );

      case 'raport z usługi':
        return (
          <select
            className={classes.formInput}
            id='worker'
            onChange={(e) => setWorker(e.target.value)}
            required
          >
            <option value=''>Wybierz z listy</option>
            {fetchedServices && fetchedServices.map((service) => (
              <option key={service.id} value={service.id}>{service.name}</option>
            ))}
          </select>
        );
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const reportData = {
      id: 0,
      serviceId: 0,
      reportType: reportType,
      description: description,
      beginDate: startDate,
      endDate: endDate,
      amount: 0,
      city: city,
      userId: userId
    };

    const response = await fetch('https://localhost:7098/api/Report', {
      method: 'post',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token,
      },
      body: JSON.stringify(reportData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      throw json({ message: 'Could not save reservation.' }, { status: 500 });
    }
    const data = await response.json();

    return navigate('/raporty');
  };

  return (
    <div className={classes.container}>
      <GridMenuHeader headerTitle="Generowanie raportu" />
      <form onSubmit={handleSubmit} className={classes.formContainer}>
        <div className={classes.formContent}>
          <div className={classes.splitSection}>
            <label htmlFor='report-type' className={classes.label}>Rodzaj raportu:</label>
            <label htmlFor='worker' className={classes.label}>Wybierz pracownika:</label>
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

            {renderSelect()}

          </div>
          <label htmlFor='start-date' className={classes.label}>Data od:</label>
          <input
            className={classes.formInput}
            id='start-date'
            type='date'
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            required
          />
          <label htmlFor='end-date' className={classes.label}>Data do:</label>
          <input
            className={classes.formInput}
            id='end-date'
            type='date'
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
            required
          />
          <label htmlFor='city' className={classes.label}>Miasto (opcj.):</label>
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
          <label htmlFor='description' className={classes.label}>Opis (opcj.):</label>
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
