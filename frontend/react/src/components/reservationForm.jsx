import classes from './reservationForm.module.css';
import * as React from 'react';
import { useNavigate, json } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faX } from '@fortawesome/free-solid-svg-icons';

const materialsData = [
    'Warszawa', 'Katowice', 'Kraków'
];

const resourcesData = [
    'typ1', 'typ2', 'typ3'
];

const workersData = [
    'Kowalski', 'Nowak', 'Marek', 'Kowalski', 'Nowak', 'Marek', 'Kowalski', 'Nowak', 'Marek',
];

export default function ReservationForm({ defaultValue, method }) {
    const [client, setClient] = React.useState(defaultValue ? defaultValue.client : '');
    const navigate = useNavigate();
    const [city, setCity] = React.useState(defaultValue ? defaultValue.city : '');
    const [startDate, setStartDate] = React.useState(defaultValue ? defaultValue.startDate : '');
    const [endDate, setEndDate] = React.useState(defaultValue ? defaultValue.endDate : '');
    const [serviceType, setServiceType] = React.useState(defaultValue ? defaultValue.typ : '');
    const [workers, setWorkers] = React.useState(defaultValue ? defaultValue.workes : workersData);
    const [materials, setMaterials] = React.useState(defaultValue ? defaultValue.materials : materialsData);
    const [resources, setResources] = React.useState(defaultValue ? defaultValue.resources : resourcesData);
    const [fetchedWorkers, setFetchedWorkers] = React.useState(workersData);
    const [fetchedMaterials, setFetchedMaterials] = React.useState(materialsData);
    const [fetchedResources, setFetchedResources] = React.useState(resourcesData);
    const [currPopupType, setPopupType] = React.useState(null);
    const [popupOpen, setPopupOpen] = React.useState('');

    //odkomentowac jak bedzie polaczone z backendem
    /* const fetchData = React.useCallback(async () => {
         try {
             const response = await fetch('');
             if (!response.ok) {
                 throw new Error('Something went wrong!');
             }
 
             const data = await response.json();
             setFetchedWorkers(data);
 
         } catch (error) {
             //setError("Something went wrong, try again.");
         }
         try {
             const response = await fetch('');
             if (!response.ok) {
                 throw new Error('Something went wrong!');
             }
 
             const data = await response.json();
             setFetchedResources(data);
 
         } catch (error) {
             //setError("Something went wrong, try again.");
         }
         try {
             const response = await fetch('');
             if (!response.ok) {
                 throw new Error('Something went wrong!');
             }
 
             const data = await response.json();
             setFetchedMaterials(data);
 
         } catch (error) {
             //setError("Something went wrong, try again.");
         }
     }, []);
 
     React.useEffect(() => {
         fetchData();
     }, [fetchData]); */

    //Handle form submit - request
    const handleSubmit = async (e) => {
        e.preventDefault();
        const reservationData = {

        };
        let url = '';

        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(reservationData),
        });

        if (response.status === 422) {
            return response;
        }

        if (!response.ok) {
            throw json({ message: 'Could not save reservation.' }, { status: 500 });
        }
        const data = await response.json();
        return navigate('/rezerwacje');
    };

    const handlePopupOpen = (e, type) => {
        e.preventDefault();
        setPopupOpen(true)
        setPopupType(type);
    };

    const handlePopup = () => {
        if (popupOpen) {
            switch (currPopupType) {
                case 'workers':
                    return (
                        <div className={classes.popupContainer}>
                            <h1>Dodaj pracownika</h1>
                            <select
                                onChange={(e) => { setWorkers([...workers, e.target.value]); setPopupOpen(false) }}
                                className={classes.formInput}
                            >
                                <option value=''>Wybierz z listy</option>
                                {fetchedWorkers.map((worker) => {
                                    return (
                                        <option key={worker} value={worker}>
                                            {worker}
                                        </option>
                                    )
                                })}
                            </select>
                        </div>
                    );
                case 'resources':
                    return (
                        <div className={classes.popupContainer}>
                            <h1>Dodaj zasoby</h1>
                            <select
                                onChange={(e) => { setResources([...resources, e.target.value]); setPopupOpen(false) }}
                                className={classes.formInput}
                            >
                                <option value=''>Wybierz z listy</option>
                                {fetchedResources.map((resource) => {
                                    return (
                                        <option key={resource} value={resource}>
                                            {resource}
                                        </option>
                                    )
                                })}
                            </select>
                        </div>
                    );
                case 'materials':
                    return (
                        <div className={classes.popupContainer}>
                            <h1>Dodaj materiały</h1>
                            <select
                                onChange={(e) => { setMaterials([...materials, e.target.value]); setPopupOpen(false) }}
                                className={classes.formInput}
                            >
                                <option value=''>Wybierz z listy</option>
                                {fetchedMaterials.map((material) => {
                                    return (
                                        <option key={material} value={material}>
                                            {material}
                                        </option>
                                    )
                                })}
                            </select>
                        </div>
                    );

            }
        }

    };

    return (
        <form className={classes.formContainer}>
            <span
                className={classes.backdrop}
                style={{ display: popupOpen ? 'block' : 'none' }}
                onClick={() => setPopupOpen(false)} />
            {handlePopup()}
            <div className={classes.splitContainer}>
                <section className={classes.leftElements}>
                    <label htmlFor='service-type' className={classes.label}>Typ usługi</label>
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
                        <label htmlFor='start-date' className={classes.label}>Data od:</label>
                        <label htmlFor='end-date' className={classes.label}>Data do:</label>
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
                    <label htmlFor='city' className={classes.label}>Miasto:</label>
                    <input
                        className={classes.formInput}
                        id='city'
                        type='text'
                        value={city}
                        placeholder='Dodaj miasto...'
                        onChange={(e) => setCity(e.target.value)}
                        required
                    />
                    <label htmlFor='client' className={classes.label}>Klient:</label>
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
                        <button
                            className={classes.actionBtn}
                            onClick={(e) => handlePopupOpen(e, 'workers')}>
                            + Dodaj pracownika
                        </button>
                        <p>Zespół wykonawczy:</p>
                        {workers &&
                            <ul className={classes.list}>
                                {workers.map((worker) => (
                                    <li>
                                        {worker}
                                        <FontAwesomeIcon
                                            icon={faX}
                                            onClick={() => {
                                                setWorkers(
                                                    workers.filter(a =>
                                                        a !== worker
                                                    )
                                                );
                                            }} />
                                    </li>
                                ))}
                            </ul>}
                        {!workers &&
                            <p>Brak danych</p>
                        }
                    </div>
                    <div className={classes.actionSection}>
                        <button
                            className={classes.actionBtn}
                            onClick={(e) => handlePopupOpen(e, 'resources')}>
                            + Dodaj zasoby
                        </button>
                        <p>Przydzielone zasoby:</p>
                        {resources &&
                            <ul className={classes.list}>
                                {resources.map((resource) => (
                                    <li>
                                        {resource}
                                        <FontAwesomeIcon
                                            icon={faX}
                                            onClick={() => {
                                                setResources(
                                                    resources.filter(a =>
                                                        a !== resource
                                                    )
                                                );
                                            }} />
                                    </li>
                                ))}
                            </ul>}
                        {!resources &&
                            <p>Brak danych</p>
                        }
                    </div>
                    <div className={classes.actionSection}>
                        <button
                            className={classes.actionBtn}
                            onClick={(e) => handlePopupOpen(e, 'materials')}>
                            + Dodaj materiały
                        </button>
                        <p>Materiały:</p>
                        {materials &&
                            <ul className={classes.list}>
                                {materials.map((material) => (
                                    <li>
                                        {material}
                                        <FontAwesomeIcon
                                            icon={faX}
                                            onClick={() => {
                                                setMaterials(
                                                    materials.filter(a =>
                                                        a !== material
                                                    )
                                                );
                                            }} />
                                    </li>
                                ))}
                            </ul>}
                        {!materials &&
                            <p>Brak danych</p>
                        }
                    </div>
                </section>
            </div>
            <button type='submit' className={classes.submitBtn} onClick={handleSubmit}>Utwórz rezerwację</button>
        </form>

    );
}