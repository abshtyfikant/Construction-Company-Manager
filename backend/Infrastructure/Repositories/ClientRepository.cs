using Domain.Interfaces.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
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
            if (client != null)
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
            throw new NotImplementedException();
        }
    }
}
