const reportsData = [];

const startDate = new Date('2023-05-01');

for (let i = 1; i <= 200; i++) {
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
