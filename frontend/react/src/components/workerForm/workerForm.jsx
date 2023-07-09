import './workerForm.css';
import * as React from 'react';
import { useNavigate, json } from 'react-router-dom';

export default function WorkerForm({ defaultValue, method }) {
    const token = localStorage.getItem('token');
    const navigate = useNavigate();
    const firstNameRef = React.useRef();
    const lastNameRef = React.useRef();
    const cityRef = React.useRef();
    const hourlyRateRef = React.useRef();
    const [specialization, setSpecialization] = React.useState(defaultValue ? (defaultValue.mainSpecializationId ? defaultValue.mainSpecializationId : undefined) : undefined);
    const [fetchedSpecializations, setFetchedSpecializations] = React.useState();

    const fetchData = React.useCallback(async () => {
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

    const handleSubmit = async (event) => {
        event.preventDefault();
        const workerData = {
            id: 0,
            firstName: firstNameRef.current.value,
            lastName: lastNameRef.current.value,
            city: cityRef.current.value,
            ratePerHour: Number(hourlyRateRef.current.value),
            mainSpecializationId: specialization,
        };

        const response = await fetch('https://localhost:7098/api/Employee', {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token,
            },
            body: JSON.stringify(workerData),
        });

        if (response.status === 422) {
            return response;
        }

        if (!response.ok) {
            throw json({ message: 'Could not save worker.' }, { status: 500 });
        }
        const data = await response.json();
        return navigate('/pracownicy'); // Przekierowanie po dodaniu pracownika
    };

    return (
        <div>
            <div className='container'>
                <form onSubmit={handleSubmit}>
                    <div className="label-container">
                        <div className="column">
                            <label>
                                Imię:
                                <input
                                    type="text"
                                    name="firstName"
                                    ref={firstNameRef}
                                    defaultValue={defaultValue ? defaultValue.firstName : null}
                                    disabled={method === 'patch' ? true : false}
                                />
                            </label>
                            <label>
                                Nazwisko:
                                <input
                                    type="text"
                                    name="lastName"
                                    ref={lastNameRef}
                                    defaultValue={defaultValue ? defaultValue.lastName : null}
                                    disabled={method === 'patch' ? true : false}
                                />
                            </label>
                            <label>
                                Miasto:
                                <input
                                    type="text"
                                    name="city"
                                    ref={cityRef}
                                    defaultValue={defaultValue ? defaultValue.city : null}
                                />
                            </label>
                            <label>
                                Specjalizacja:
                            </label>
                            <select
                                onChange={(e) => { setSpecialization(e.target.value) }}
                                value={specialization}
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
                            <label>
                                Stawka godzinowa (zł/h):
                                <input
                                    type="text"
                                    name="hourlyRate"
                                    ref={hourlyRateRef}
                                    defaultValue={defaultValue ? defaultValue.ratePerHour : null}
                                />
                            </label>
                        </div>
                    </div>
                    <div className="button-container">
                        <button type="submit">Dodaj pracownika</button>
                    </div>
                </form>
            </div>
        </div>
    );
}