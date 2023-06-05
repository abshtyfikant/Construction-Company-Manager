import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';


function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = () => {
    if (username === 'admin' && password === 'admin') {
      console.log('Zalogowano pomyślnie');
      navigate('/menu'); // Ścieżka do zakładki z kafelkami
    } else {
      console.log('Błąd logowania');
      alert('Błąd logowania');
    }
  };

  return (
    <section className='center-container'>
      <div className="logowanie">
        <h1>Logowanie</h1>
        <input
          type="text"
          placeholder="Adres e-mail"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <br />
        <input
          type="password"
          placeholder="Hasło"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <br />
        <button onClick={handleLogin}>Zaloguj</button>
      </div>
    </section>

  );
}

export default Login;
