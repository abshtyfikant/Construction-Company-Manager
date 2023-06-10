const resourcesData = [];

for (let i = 1; i <= 200; i++) {
  const resource = {
    id: i,
    resourceName: `Nazwa ${i}`,
    quantinity: i,
  };

  resourcesData.push(resource);
}

export default resourcesData;
