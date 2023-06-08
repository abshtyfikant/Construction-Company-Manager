import GridMenuHeader from '../components/gridMenuHeader';
import classes from './styles/reservationsForm.module.css';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

const materialsData = [
  'Warszawa', 'Katowice', 'Kraków'
];

const resourcesData = [
  'typ1', 'typ2', 'typ3'
];

const workersData = [
  'Kowalski', 'Nowak', 'Marek','Kowalski', 'Nowak', 'Marek','Kowalski', 'Nowak', 'Marek',
];

function ReservationsForm() {
  const [client, setClient] = React.useState('');
  const navigate = useNavigate();
  const [city, setCity] = React.useState('');
  const [startDate, setStartDate] = React.useState('');
  const [endDate, setEndDate] = React.useState('');
  const [serviceType, setServiceType] = React.useState('');
  const [workers, setWorkers] = React.useState(workersData);
  const [materials, setMaterials] = React.useState(materialsData);
  const [resources, setResources] = React.useState(resourcesData);

  return (
    <div className={classes.container}>
      <GridMenuHeader headerTitle="Formularz rezerwacji" />
      <form className={classes.formContainer}>
        <div className={classes.splitContainer}>
          <section className={classes.leftElements}>
            <label for='service-type' className={classes.label}>Typ usługi</label>
            <input
              className={classes.formInput}
              id='service-type'
              type='text'
              value={serviceType}
              placeholder='Dodaj typ usługi...'
              onChange={(e) => setServiceType(e.target.value)}
              required
            />
            <div className={classes.dateSection}>
              <label for='start-date' className={classes.label}>Data od:</label>
              <label for='end-date' className={classes.label}>Data do:</label>
              <input
                className={classes.formInput}
                id='start-date'
                type='date'
                value={startDate}
                onChange={(e) => setStartDate(e.target.value)}
                required
              />
              <input
                className={classes.formInput}
                id='end-date'
                type='date'
                value={endDate}
                onChange={(e) => setEndDate(e.target.value)}
                required
              />
            </div>
            <label for='city' className={classes.label}>Miasto:</label>
            <input
              className={classes.formInput}
              id='city'
              type='text'
              value={city}
              placeholder='Dodaj miasto...'
              onChange={(e) => setCity(e.target.value)}
              required
            />
            <label for='client' className={classes.label}>Klient:</label>
            <input
              className={classes.formInput}
              id='client'
              type='text'
              value={client}
              placeholder='Dodaj klienta...'
              onChange={(e) => setClient(e.target.value)}
              required
            />
          </section>
          <section className={classes.rightElements}>
            <div className={classes.actionSection}>
              <button className={classes.actionBtn}>+ Dodaj pracowników</button>
              <p>Zespół wykonawczy:</p>
              {workers &&
                <ul className={classes.list}>
                  {workers.map((worker) => (
                    <li>{worker}</li>
                  ))}
                </ul>}
                {!workers &&
               <p>Brak danych</p>
               }
            </div>
            <div className={classes.actionSection}>
              <button className={classes.actionBtn}>+ Przydziel zasoby</button>
              <p>Przydzielone zasoby:</p>
              {resources &&
                <ul className={classes.list}>
                  {resources.map((resource) => (
                    <li>{resource}</li>
                  ))}
                </ul>}
                {!resources &&
               <p>Brak danych</p>
               }
            </div>
            <div className={classes.actionSection}>
              <button className={classes.actionBtn}>+ Dodaj materiały</button>
              <p>Materiały:</p>
              {materials &&
                <ul className={classes.list}>
                  {materials.map((material) => (
                    <li>{material}</li>
                  ))}
                </ul>}
                {!materials &&
               <p>Brak danych</p>
               }
            </div>
          </section>
        </div>
        <button type='submit' className={classes.submitBtn}>Utwórz rezerwację</button>
      </form>
    </div>
  );
}

export default ReservationsForm;
