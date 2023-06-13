import GridMenuHeader from '../components/gridMenuHeader';
import '../css/remove-worker.css';
import * as React from 'react';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function RemoveWorker() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    id: '',
    firstName: '',
    lastName: '',
 
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
    navigate(''); // Przekierowanie po usunieciu pracownika
  };

  return (
    <div>
        <GridMenuHeader headerTitle="Usuń pracownika" />
        <div className='container'>
            <form onSubmit={handleSubmit}>
            <div className="label-container">
                <div className="column">
                    <label>
                    ID :
                    <input type="text" name="id" value={formData.id} onChange={handleChange} />
                    </label>

                    <label>
                    Imię: 
                    <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} />
                    </label>
                    <label>
                    Nazwisko:
                    <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} />
                    </label>
                  
                </div>
            </div>
            <div className="button-container">
                <button type="submit">Usuń pracownika</button>
            </div>
            </form>
        </div>
    </div>
  );
}

export default RemoveWorker;
