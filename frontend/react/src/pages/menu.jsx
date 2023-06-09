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
          <div>
            <Link className='menu-element' to="/raporty">Raporty</Link>
            <Link to='/generowanie-raportu'><GraySquareWithPlus /></Link>
          </div>
          {/* <div><Link to="/generowanie-raportu">Generowanie raportu</Link> <GraySquareWithPlus /></div> */}
          <div>
            <Link className='menu-element' to="/rezerwacje">Rezerwacje</Link>
            <Link to='/formularz-rezerwacji'><GraySquareWithPlus /></Link>
          </div>
          <div>
            <Link className='menu-element' to="/formularz-rezerwacji">Formularz rezerwacji</Link>
          </div>
          <div>
            <Link className='menu-element' to="/stan-zasobow">Stan zasobów</Link>
          </div>
          <div>
            <Link className='menu-element' to="/pracownicy">Pracownicy</Link>
          </div>
        </nav>
      </section>
    </section>
  );
}

export default Menu;
