import GridMenuHeader from "../../components/gridMenuHeader";
import ReservationForm from "../../components/reservationForm/reservationForm";
import classes from "./reservationsForm.module.css";
import * as React from "react";

function ReservationsForm() {
  return (
    <div className={classes.container}>
      <GridMenuHeader headerTitle="Formularz rezerwacji" />
      <ReservationForm method={"post"} />
    </div>
  );
}

export default ReservationsForm;
