using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IClientRepository
{
    void DeleteClient(int clientId);
    int AddClient(Client client);
    IQueryable<Client> GetAllClients();
    Client GetClient(int clientId);
    void UpdateClient(Client client);
}