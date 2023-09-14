import WorkerForm from "../components/workerForm/workerForm";
import "../css/add-worker.css";
import * as React from "react";
import { useLocation } from "react-router-dom";
import GridMenuHeader from "../components/gridMenuHeader";

function EditWorker() {
  const token = localStorage.getItem("token");
  const location = useLocation();

  return (
    <div>
      <GridMenuHeader headerTitle="Edytuj pracownika" />
      <WorkerForm defaultValue={location.state.worker} method={"put"} />
    </div>
  );
}

export default EditWorker;
