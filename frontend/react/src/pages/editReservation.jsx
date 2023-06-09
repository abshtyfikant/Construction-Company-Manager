import GridMenuHeader from "../components/gridMenuHeader";
import * as React from 'react';
import ReservationForm from "../components/reservationForm";
import { useLocation } from "react-router-dom";
import classes from './styles/editReservation.module.css';

export default function EditReservation () {
    const location = useLocation();
    console.log(location.state.reservation);
    return (
        <div className={classes.container}>
            <GridMenuHeader headerTitle="Edytowanie raportu" />
            <ReservationForm defaultValue={location.state.reservation} method={"patch"} />
        </div>
    );
}