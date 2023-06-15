using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ClientRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int AddClient(Client client)
    {
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
        return client.Id;
    }

    public void DeleteClient(int clientId)
    {
        var client = _dbContext.Clients.Find(clientId);
        if (client is not null)
        {
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }
    }

    public IQueryable<Client> GetAllClients()
    {
        var clients = _dbContext.Clients;
        return clients;
    }

    public Client GetClient(int clientId)
    {
        var client = _dbContext.Clients.FirstOrDefault(i => i.Id == clientId);
        return client;
    }

    public void UpdateClient(Client client)
    {
        _dbContext.Attach(client);
        _dbContext.Entry(client).Property("FirstName").IsModified = true;
        _dbContext.Entry(client).Property("LastName").IsModified = true;
        _dbContext.Entry(client).Property("City").IsModified = true;
        _dbContext.SaveChanges();
    }
}