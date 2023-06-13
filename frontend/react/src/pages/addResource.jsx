import GridMenuHeader from '../components/gridMenuHeader';
import '../css/add-resource.css';
import * as React from 'react';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function AddResource() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    
    resourceName: '',
    quantity: '',
 
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
    navigate('/stan-zasobow'); // Przekierowanie po usunieciu pracownika
  };

  return (
    <div>
        <GridMenuHeader headerTitle=" Dodaj zasób" />
        <div className='container'>
            <form onSubmit={handleSubmit}>
            <div className="label-container">
                <div className="column">

                    <label>
                    Nazwa zasobu: 
                    <input type="text" name="resourceName" value={formData.resourceName} onChange={handleChange} />
                    </label>
                    <label>
                    Ilość:
                    <input type="text" name="quantity" value={formData.quantity} onChange={handleChange} />
                    </label>
                  
                </div>
            </div>
            <div className="button-container">
                <button type="submit">Dodaj zasób</button>
            </div>
            </form>
        </div>
    </div>
  );
}

export default AddResource;
