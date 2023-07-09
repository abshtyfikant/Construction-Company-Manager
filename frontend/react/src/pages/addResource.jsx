import GridMenuHeader from '../components/gridMenuHeader';
import '../css/add-resource.css';
import * as React from 'react';
import { useState } from 'react';
import { useNavigate, json } from 'react-router-dom';

function AddResource() {
  const token = localStorage.getItem('token');
  const navigate = useNavigate();
  const quantityRef = React.useRef();
  const nameRef = React.useRef();
  const [unit, setUnit] = React.useState();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formData = {
      id: 0,
      name: nameRef.current.value,
      quantity: quantityRef.current.value,
      //unit: unit,
    };

    const response = await fetch('https://localhost:7098/api/Resource', {
      method: 'post',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token,
      },
      body: JSON.stringify(formData),
    });

    if (response.status === 422) {
      return response;
    }

    if (!response.ok) {
      throw json({ message: 'Could not save reservation.' }, { status: 500 });
    }
    const data = await response.json();

    return navigate('/stan-zasobow');
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
                <input
                  type="text"
                  name="resourceName"
                  id='resourceName'
                  ref={nameRef}
                />
              </label>
              <label htmlFor='quantity'> Ilość: </label>
              <input
                type="text"
                name="quantity"
                id='quantity'
                ref={quantityRef}
              />
               <select
                onChange={(e) => {setUnit(e.target.value) }}
              >
                <option value=''>Wybierz z listy</option>
                <option value='m2'>m<sup>2</sup></option>
                <option value='m3'>m<sup>3</sup></option>
                <option value='m'>m</option>
                <option value='kg'>kg</option>
                <option value='szt'>szt</option>
              </select>
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
