import React, { useState } from 'react';
import { useNavigate, redirect, json, Form, Link, useNavigation } from 'react-router-dom';

function Register() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const navigate = useNavigate();
  const navigation = useNavigation();
  const isSubmitting = navigation.state === 'submitting';

  const handleRegister = () => {
    if (email === 'admin' && password === 'admin') {
      console.log('Zalogowano pomyślnie');
      navigate('/menu'); // Ścieżka do zakładki z kafelkami
    } else {
      console.log('Błąd logowania');
      alert('Błąd logowania');
    }
  };

  return (
    <section className='center-container'>
      <Form className="logowanie" method='post'>
        <h1>Zarejestruj się</h1>
        <input
          type="text"
          placeholder="Imię"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          id='firstName'
          name='firstName'
        />
         <input
          type="text"
          placeholder="Nazwisko"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          id='lastName'
          name='lastName'
        />
        <input
          type="email"
          placeholder="Adres e-mail"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          id='email'
          name='email'
        />
        <br />
        <input
          type="password"
          placeholder="Hasło"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          id='password'
          name='password'
        />
        <br />
        {/*<button onClick={handleRegister}>Zaloguj</button>*/}
        <button disabled={isSubmitting}>
          {isSubmitting ? 'Logowanie...' : 'Zaloguj'}
        </button>
      </Form>
    </section>

  );
}

export default Register;

export async function action({ request }) {
  const searchParams = new URL(request.url).searchParams;
  //const mode = searchParams.get('mode') || 'Register';

  const data = await request.formData();
  const authData = {
    firstName: data.get('firstName'),
    lastName: data.get('lastName'),
    email: data.get('email'),
    password: data.get('password'),
  };

  const response = await fetch('https://localhost:7098/auth/Register', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(authData),
  });

  if (response.status === 422 || response.status === 401) {
    return response;
  }

  if (!response.ok) {
    throw json({ message: 'Could not authenticate user.' }, { status: 500 });
  }

  const resData = await response.json();
  const token = resData.token;

  localStorage.setItem('token', token);
  const expiration = new Date();
  expiration.setHours(expiration.getHours() + 1);
  localStorage.setItem('expiration', expiration.toISOString());

  return redirect('/menu');
}