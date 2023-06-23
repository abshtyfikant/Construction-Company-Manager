const reportsData = [];

const startDate = new Date('2023-05-01');

for (let i = 1; i <= 60; i++) {
  const endDate = new Date(startDate);
  endDate.setDate(endDate.getDate() + 4);

  const report = {
    rodzaj: `Raport ${i}`,
    dataOd: formatDate(startDate),
    dataDo: formatDate(endDate),
    autor: `Autor ${i}`,
    opis: `Opis raportu losowy z liczba test raportu ${i}`
  };

  reportsData.push(report);

  startDate.setDate(startDate.getDate() + 1);
}

function formatDate(date) {
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');

  return `${year}-${month}-${day}`;
}

export default reportsData;


/*

[
    {
        "id": 6,
        "reportType": "Typ raportu 1",
        "beginDate": "2023-01-01T00:00:00",
        "endDate": "2023-01-10T00:00:00",
        "description": "Opis raportu 1",
        "author": "op op"
    },
    {
        "id": 7,
        "reportType": "Typ raportu 2",
        "beginDate": "2023-02-01T00:00:00",
        "endDate": "2023-02-15T00:00:00",
        "description": "Opis raportu 2",
        "author": "op op"
    },
    {
        "id": 8,
        "reportType": "Typ raportu 3",
        "beginDate": "2023-03-01T00:00:00",
        "endDate": "2023-03-20T00:00:00",
        "description": "Opis raportu 3",
        "author": "op op"
    },
    {
        "id": 9,
        "reportType": "Typ raportu 4",
        "beginDate": "2023-04-01T00:00:00",
        "endDate": "2023-04-30T00:00:00",
        "description": "Opis raportu 4",
        "author": "op op"
    },
    {
        "id": 10,
        "reportType": "Typ raportu 5",
        "beginDate": "2023-05-01T00:00:00",
        "endDate": "2023-05-31T00:00:00",
        "description": "Opis raportu 5",
        "author": "op op"
    }
]

*/