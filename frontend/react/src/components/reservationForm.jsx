import classes from './reservationForm.module.css';
import * as React from 'react';
import { useNavigate, json } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faX } from '@fortawesome/free-solid-svg-icons';
//dodac wybierz albo dodaj nowego klienta
const materialsData = [
    'Warszawa', 'Katowice', 'Kraków'
];

const resourcesData = [
    'typ1', 'typ2', 'typ3'
];

const workersData = [
    'Kowalski', 'Nowak', 'Marek', 'Kowalski', 'Nowak', 'Marek', 'Kowalski', 'Nowak', 'Marek',
];

const clientsData = [
    'Kowalski', 'Nowak', 'Marek', 'Kowalski', 'Nowak', 'Marek', 'Kowalski', 'Nowak', 'Marek',
];

const specializationsData = [
    'murarz', 'tynkarz', 'akrobata',
];

export default function ReservationForm({ defaultValue, method }) {
    const token = localStorage.getItem('token');
    const [client, setClient] = React.useState(defaultValue ? defaultValue.client : '');
    const clientFirstNameRef = React.useRef();
    const clientLastNameRef = React.useRef();
    const clientCityRef = React.useRef();
    const navigate = useNavigate();
    const [city, setCity] = React.useState(defaultValue ? defaultValue.city : '');
    const [startDate, setStartDate] = React.useState(defaultValue ? defaultValue.beginDate : '');
    const [endDate, setEndDate] = React.useState(defaultValue ? defaultValue.endDate : '');
    const [serviceType, setServiceType] = React.useState(defaultValue ? defaultValue.serviceType : '');
    const [workers, setWorkers] = React.useState(defaultValue ? defaultValue.workes : {});
    const [materials, setMaterials] = React.useState(defaultValue ? defaultValue.materials : {});
    const [resources, setResources] = React.useState(defaultValue ? defaultValue.resources : {});
    const [fetchedWorkers, setFetchedWorkers] = React.useState();
    const [fetchedResources, setFetchedResources] = React.useState();
    const [fetchedClients, setFetchedClients] = React.useState();
    const [fetchedSpecializations, setFetchedSpecializations] = React.useState();
    const [currPopupType, setPopupType] = React.useState(null);
    const [popupOpen, setPopupOpen] = React.useState('');
    const resourceNameRef = React.useRef();
    const resourcePriceRef = React.useRef();
    const resourceAmountRef = React.useRef();
    const resourceUnitRef = React.useRef();

    //odkomentowac jak bedzie polaczone z backendem
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
            const response = await fetch('', { //https://localhost:7098/api/Resource
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
            setFetchedResources(data);

        } catch (error) {
            //setError("Something went wrong, try again.");
        }

        try {
            const response = await fetch('https://localhost:7098/api/Client', {
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
            setFetchedClients(data);

        } catch (error) {
            //setError("Something went wrong, try again.");
        }

        try {
            const response = await fetch('https://localhost:7098/api/Specialization', {
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
            setFetchedSpecializations(data);

        } catch (error) {
            //setError("Something went wrong, try again.");
        }
    }, []);

    React.useEffect(() => {
        fetchData();
    }, [fetchData]);

    const submitClient = async () => {
        if (!client && clientFirstNameRef && clientLastNameRef && clientCityRef) {
            setClient({
                id: 0,
                firstName: clientFirstNameRef.current.value,
                lastName: clientLastNameRef.current.value,
                city: clientCityRef.current.value,
            });

            const response = await fetch('', {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token,
                },
                body: JSON.stringify(client),
            });

            if (response.status === 422) {
                return response;
            }

            if (!response.ok) {
                throw json({ message: 'Could not save client.' }, { status: 500 });
            }
            const data = await response.json();
            return data;
        }
    };

    const submitReservation = async (clientId) => {
        const reservationData = {
            id: 0,
            clientId: clientId,
            serviceType: serviceType,
            beginDate: startDate,
            endDate: endDate,
            city: city,
            serviceStatus: "",
            paymentStatus: "",
        };

        const response = await fetch('', {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token,
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
        return data;
    };

    const submitMaterials = async (serviceId) => {
        let data = [];
        materials.forEach(async material => {
            material.push({
                id: 0,
                serviceId: serviceId,
            });

            const response = await fetch('', {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token,
                },
                body: JSON.stringify(material),
            });

            if (response.status === 422) {
                return response;
            }

            if (!response.ok) {
                throw json({ message: 'Could not save material.' }, { status: 500 });
            }
            data = [...data, await response.json()];
            return data;
        });
    };

    const submitResourcesAllocation = async (reservationId) => {
        let data = [];
        let resourcesAllocationData = {};
        resources.forEach(async (resource) => {
            resourcesAllocationData = {
                resourceId: resource.id,
                serviceId: reservationId,
                allocatedQuantinity: resource.quantinty,
                beginDate: startDate,
                endDate: endDate,
            };

            const response = await fetch('', {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token,
                },
                body: JSON.stringify(resourcesAllocationData),
            });

            if (response.status === 422) {
                return response;
            }

            if (!response.ok) {
                throw json({ message: 'Could not save materials.' }, { status: 500 });
            }
            data = [...data, await response.json()];
        });
        return data;
    };

    const submitWorkersAllocation = async (reservationId) => {
        let data = [];
        let workersAllocationData = {};
        workers.forEach(async (worker) => {
            workersAllocationData = {
                employeeId: worker.id,
                serviceId: reservationId,
                function: worker.function,
            };


            const response = await fetch('', {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token,
                },
                body: JSON.stringify(workersAllocationData),
            });

            if (response.status === 422) {
                return response;
            }

            if (!response.ok) {
                throw json({ message: 'Could not save workers.' }, { status: 500 });
            }
            data = [...data, await response.json()];
        });
        return data;
    };

    //Handle form submit - request
    const handleSubmit = async (e) => {
        e.preventDefault();

        const clientId = await submitClient().id;
        if (clientId != undefined) {
            var reservationId = await submitReservation(clientId);
            if (reservationId != undefined) {
                const response = await Promise.allSettled([submitMaterials(reservationId), submitResourcesAllocation(reservationId),
                submitWorkersAllocation(reservationId)]);
            } else {
                throw json({ message: 'Something went wrong.' }, { status: 500 });
            }
        } else {
            throw json({ message: 'Something went wrong.' }, { status: 500 });
        }

        return navigate('/rezerwacje');
    };

    const handlePopupOpen = (e, type) => {
        e.preventDefault();
        setPopupOpen(true)
        setPopupType(type);
    };

    const handleAddMaterial = (e) => {
        e.preventDefault();
        const material = {
            id: 0,
            name: resourceNameRef.current.value,
            unit: resourceUnitRef.current.value,
            price: resourceUnitRef.current.value,
            quantinty: resourceAmountRef.current.value,
        }
        setMaterials([...materials, material]);
        setPopupOpen(false);
    };

    const handlePopup = () => {
        if (popupOpen) {
            switch (currPopupType) {
                case 'workers':
                    return (
                        <div className={classes.popupContainer}>
                            <div>
                                <h1>Dodaj pracownika</h1>
                                <select
                                    onChange={(e) => { }}
                                    className={classes.formInput}
                                >
                                    <option value=''>Wybierz z listy</option>
                                    {fetchedWorkers && fetchedWorkers.map((worker) => {
                                        return (
                                            <option key={worker.id} value={worker}>
                                                {worker.firstName} {worker.lastName}
                                            </option>
                                        )
                                    })}
                                </select>

                                <select
                                    onChange={(e) => { }}
                                    className={classes.formInput}
                                >
                                    <option value=''>Wybierz z listy</option>
                                    {fetchedSpecializations && fetchedSpecializations.map((specialization) => {
                                        return (
                                            <option key={specialization.id} value={specialization.id}>
                                                {specialization.name}
                                            </option>
                                        )
                                    })}
                                </select>
                                <button
                                    onClick={(e) => {
                                        e.preventDefault();
                                        setPopupOpen(false);
                                    }}>
                                    Dodaj pracownika
                                </button>
                            </div>
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
                                {fetchedResources && fetchedResources.map((resource) => {
                                    return (
                                        <option key={resource.id} value={resource}>
                                            {resource.name}
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
                            <label htmlFor='resource-name' className={classes.label}>Nazwa:</label>
                            <input
                                className={classes.formInput}
                                id='resource-name'
                                type='text'
                                ref={resourceNameRef}
                                required
                            />
                            <label htmlFor='resource-price' className={classes.label}>Cena:</label>
                            <input
                                className={classes.formInput}
                                id='resource-price'
                                type='text'
                                ref={resourcePriceRef}
                                required
                            />
                            <label htmlFor='resource-amount' className={classes.label}>Ilość:</label>
                            <input
                                className={classes.formInput}
                                id='resource-amount'
                                type='text'
                                ref={resourceAmountRef}
                                required
                            />
                            <label htmlFor='resource-unit' className={classes.label}>Jednostka:</label>
                            <input
                                className={classes.formInput}
                                id='resource-unit'
                                type='text'
                                ref={resourceUnitRef}
                                required
                            />
                            <button onClick={handleAddMaterial}>Dodaj materiał</button>
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
                    <h2>Usługa</h2>
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
                    <h2>Klient</h2>
                    <p>Wybierz klienta z listy:</p>
                    <select
                        onChange={(e) => { setClient(e.target.value) }}
                        className={classes.formInput}
                    >
                        <option value=''>Wybierz z listy</option>
                        {fetchedClients && fetchedClients.map((client) => {
                            return (
                                <option key={client.id} value={client}>
                                    {client.firstName} {client.lastName}
                                </option>
                            )
                        })}
                    </select>
                    <p>Lub dodaj nowego:</p>
                    <label htmlFor='clientFirstName' className={classes.label}>Imię:</label>
                    <input
                        className={classes.formInput}
                        id='clientFirstName'
                        type='text'
                        //value={clientFirstName}
                        placeholder='Dodaj imię klienta...'
                        ref={clientFirstNameRef}
                        required
                        disabled={client ? true : false}
                    />
                    <label htmlFor='clientLastName' className={classes.label}>Nazwisko:</label>
                    <input
                        className={classes.formInput}
                        id='clientLastName'
                        type='text'
                        //value={clientLastName}
                        placeholder='Dodaj nazwisko klienta...'
                        ref={clientLastNameRef}
                        disabled={client ? true : false}
                        required
                    />
                    <label htmlFor='clientCity' className={classes.label}>Miasto:</label>
                    <input
                        className={classes.formInput}
                        id='clientCity'
                        type='text'
                        //value={clientCity}
                        placeholder='Dodaj misto klienta...'
                        ref={clientCityRef}
                        disabled={client ? true : false}
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
                        {Object.keys(workers).length !== 0 &&
                            <ul className={classes.list}>
                                {workers.map((worker) => (
                                    <li>
                                        {worker.firstName}
                                        <FontAwesomeIcon
                                            icon={faX}
                                            onClick={() => {
                                                setWorkers(
                                                    workers.filter(a =>
                                                        a.id !== worker.id
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
                        {Object.keys(resources).length !== 0 &&
                            <ul className={classes.list}>
                                {resources.map((resource) => (
                                    <li>
                                        {resource}
                                        <FontAwesomeIcon
                                            icon={faX}
                                            onClick={() => {
                                                setResources(
                                                    resources.filter(a =>
                                                        a.id !== resource.id
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
                        {Object.keys(materials).length !== 0 &&
                            <ul className={classes.list}>
                                {materials.map((material) => (
                                    <li>
                                        {material}
                                        <FontAwesomeIcon
                                            icon={faX}
                                            onClick={() => {
                                                setMaterials(
                                                    materials.filter(a =>
                                                        a.id !== material.id
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