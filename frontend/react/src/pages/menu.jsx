import React from 'react';
import { Link } from 'react-router-dom';
import GraySquareWithPlus from '../components/square';

function Menu() {
  return (
    <section className='menu'>
      <div className='grid-3'>
        <div className='item'></div>
        <div className='item'>
          <h1>Menu główne</h1>
        </div>
        <div className='item'>
          <Link to="/">Wyloguj się</Link>
        </div>
      </div>
      <section className='center-container'>
        <nav>
            <div><Link to="/raporty">Raporty</Link> <GraySquareWithPlus /></div>
            {/* <div><Link to="/generowanie-raportu">Generowanie raportu</Link> <GraySquareWithPlus /></div> */}
            <div><Link to="/rezerwacje">Rezerwacje</Link> <GraySquareWithPlus /></div>
            <div><Link to="/formularz-rejestracji">Formularz rezerwacji</Link></div>
            <div><Link to="/stan-zasobow">Stan zasobów</Link></div>
            <div><Link to="/pracownicy">Pracownicy</Link></div>
        </nav>
      </section>
    </section>
  );
}

export default Menu;
