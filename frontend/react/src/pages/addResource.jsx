import GridMenuHeader from '../components/gridMenuHeader';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';
import ResourceForm from '../components/resourceForm/resourceForm';

function AddResource() {
  const token = localStorage.getItem('token');
  const navigate = useNavigate();
  
  return (
    <div>
      <GridMenuHeader headerTitle="Dodaj zasób" />
      <ResourceForm method={'post'}/>
    </div>
  );
}

export default AddResource;
