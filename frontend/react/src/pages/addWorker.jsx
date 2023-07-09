import WorkerForm from '../components/workerForm/workerForm';
import '../css/add-worker.css';
import * as React from 'react';
import GridMenuHeader from '../components/gridMenuHeader';

function AddWorker() {
  const token = localStorage.getItem('token');

  return (
    <div>
      <GridMenuHeader headerTitle="Dodaj pracownika" />
      <WorkerForm method={'post'} />
    </div>
  );
}

export default AddWorker;
