import './resourceForm.css';
import { useNavigate, json } from 'react-router-dom';
import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

export default function ResourceForm({ defaultValue, method }) {
  const token = localStorage.getItem('token');
  const navigate = useNavigate();
  const quantityRef = React.useRef();
  const nameRef = React.useRef();
  const [unit, setUnit] = React.useState(defaultValue ? (defaultValue.unit ? defaultValue.unit : undefined) : undefined);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formData = {
      id: 0,
      name: nameRef.current.value,
      quantity: quantityRef.current.value,
      //unit: unit,
    };

    const response = await fetch('https://localhost:7098/api/Resource', {
      method: method,
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

  const handleDelete = async (e) => {
    const response = await fetch('https://localhost:7098/api/Resource/' + defaultValue.id, {
      method: 'delete',
      headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token,
      },
  });

  if (!response.ok) {
      // return { isError: true, message: 'Could not fetch project.' };
      // throw new Response(JSON.stringify({ message: 'Could not fetch project.' }), {
      //   status: 500,
      // });
      throw json(
          { message: 'Could not fetch resource.' },
          {
              status: 500,
          }
      );
  } else {
      return navigate('/stan-zasobow');
  }
  };

  return (
    <div>
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
                  defaultValue={defaultValue ? defaultValue.name : null}
                />
              </label>
              <label htmlFor='quantity'> Ilość: </label>
              <input
                type="text"
                name="quantity"
                id='quantity'
                ref={quantityRef}
                defaultValue={defaultValue ? defaultValue.quantity : null}
              />
              <select
                onChange={(e) => { setUnit(e.target.value) }}
                value={unit}
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
            <button type="submit">Zatwierdź</button>
            {method === 'patch' &&
              <button onClick={(e) => { handleDelete(e) }}>
                <FontAwesomeIcon icon={faTrash} />
              </button>}
              {/* {//zrobic sprawdzanie czy nie jest zarezerwowane} */}

          </div>
        </form>
      </div>
    </div>
  );
}