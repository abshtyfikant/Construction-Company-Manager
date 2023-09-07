import React, { useState } from 'react';
import { useNavigate, redirect, json, Form, Link, useNavigation } from 'react-router-dom';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const navigation = useNavigation();
  const isSubmitting = navigation.state === 'submitting';

  const handleLogin = () => {
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
        <h1>Logowanie</h1>
        <input
          type="text"
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
        {/*<button onClick={handleLogin}>Zaloguj</button>*/}
        <button disabled={isSubmitting}>
          {isSubmitting ? 'Logowanie...' : 'Zaloguj'}
        </button>
      </Form>
    </section>

  );
}

export default Login;

export async function action({ request }) {
  const searchParams = new URL(request.url).searchParams;
  //const mode = searchParams.get('mode') || 'login';

  const data = await request.formData();
  const authData = {
    email: data.get('email'),
    password: data.get('password'),
  };

  const response = await fetch('https://localhost:7098/auth/Login', {
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
  const expiration = new Date();
  expiration.setTime(expiration.getTime() + (1*60*60*1000));
  localStorage.setItem('token', resData.token);
  localStorage.setItem('expiration', expiration)
  localStorage.setItem('userId', resData.id);

  return redirect('/menu');
}