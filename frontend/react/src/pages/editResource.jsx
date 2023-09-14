import GridMenuHeader from "../components/gridMenuHeader";
import * as React from "react";
import { useLocation } from "react-router-dom";
import ResourceForm from "../components/resourceForm/resourceForm";

function EditResource() {
  const token = localStorage.getItem("token");
  const location = useLocation();

  return (
    <div>
      <GridMenuHeader headerTitle="Edytuj zasób" />
      <ResourceForm defaultValue={location.state.resource} method={"put"} />
    </div>
  );
}

export default EditResource;
