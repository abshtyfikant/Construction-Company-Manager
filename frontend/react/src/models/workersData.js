const workersData = [];

for (let i = 1; i <= 200; i++) {
  const worker = {
    id: i,
    firstName: `Imię ${i}`,
    lastName: `Nazwisko ${i}`,
    city: `Miasto ${i}`,
    workerSpecializationId: i,
    ratePerHour: i,
  };

  workersData.push(worker);
}

export default workersData;
