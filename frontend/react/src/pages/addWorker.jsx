import GridMenuHeader from '../components/gridMenuHeader';
import '../css/add-worker.css';
import * as React from 'react';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function AddWorker() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({

    firstName: '',
    lastName: '',
    city: '',
    specialization: '',
    hourlyRate: ''
  });

  const handleChange = (event) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    // Wysyłanie danych pracownika do serwera lub innego przetwarzania
    console.log(formData);
    navigate('/pracownicy'); // Przekierowanie po dodaniu pracownika
  };

  return (
    <div>
        <GridMenuHeader headerTitle="Dodaj pracownika" />
        <div className='container'>
            <form onSubmit={handleSubmit}>
            <div className="label-container">
                <div className="column">
                    <label>
                    Imię: 
                    <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} />
                    </label>
                    <label>
                    Miasto:
                    <input type="text" name="city" value={formData.city} onChange={handleChange} />
                    </label>
                    <label>
                    Nazwisko:
                    <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} />
                    </label>
                    <label>
                    Specjalizacja:
                    <input type="text" name="specialization" value={formData.specialization} onChange={handleChange} />
                    </label>
                    <label>
                    Stawka godzinowa:
                    <input type="text" name="hourlyRate" value={formData.hourlyRate} onChange={handleChange} />
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

export default AddWorker;
