import classes from '../pages/styles/reportDetails.module.css';
import { useRouteLoaderData, defer, json, useNavigate } from 'react-router-dom';

//dodac na backendzie id pracownika

export default function ReportDetails() {
    const navigate = useNavigate();
    const { report } = useRouteLoaderData('report-details');

    return (
        <div className={classes.container}>
            <h1>Raport</h1>
            <p>Typ raportu: {report.reportType}</p>
            <div className={classes.dates}>
                <p>Data od: </p>
                {report.beginDate}
                <p>Data do: </p>
                {report.endDate}
            </div>
            <p>Miasto: {report.city}</p>
            <p>Opis: </p>
            <p>{report.description}</p>
            <p>Suma: {report.amount}</p>
            <p>Wygenerowano przez: { }</p>
        </div>
    );
}

async function loadServiceDetails(id, token) {
    const response = await fetch('https://localhost:7098/api/Service/' + id, {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        }
    });

    if (!response.ok) {
        throw json(
            { message: 'Could not fetch report.' },
            {
                status: 500,
            }
        );
    } else {
        const serviceData = await response.json();
        return serviceData;
    }
}

async function loadReportDetails(id) {
    const token = localStorage.getItem('token');
    const response = await fetch('https://localhost:7098/api/Report/' + id, {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        }
    });

    if (!response.ok) {
        throw json(
            { message: 'Could not fetch report.' },
            {
                status: 500,
            }
        );
    } else {
        const reportData = await response.json();

        const serviceData = await loadServiceDetails(reportData.serviceId, token);

        reportData.service = serviceData;

        return reportData;
    }
}

export async function loader({ request, params }) {
    const id = params.reportId;
    return defer({
        report: await loadReportDetails(id),
    });
}