import React from 'react';
import { Link } from 'react-router-dom';

const GridMenuHeader = ({ headerTitle }) => {
  return (
    <div className='grid-3'>
      <div className='item'>
        <Link to="/menu">&lt;Powrót</Link>
      </div>
      <div className='item'>
        <h1>{headerTitle}</h1>
      </div>
      <div className='item'></div>
    </div>
  );
};

export default GridMenuHeader;
