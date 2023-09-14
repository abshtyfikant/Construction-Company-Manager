import GridMenuHeader from "../../components/gridMenuHeader";
import * as React from "react";
import ReservationForm from "../../components/reservationForm/reservationForm";
import { useLocation } from "react-router-dom";
import classes from "./editReservation.module.css";

export default function EditReservation() {
  const location = useLocation();

  return (
    <div className={classes.container}>
      <GridMenuHeader headerTitle="Edytowanie raportu" />
      <ReservationForm
        defaultValue={location.state.reservation}
        method={"put"}
      />
    </div>
  );
}
