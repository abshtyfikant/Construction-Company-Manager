using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IClientRepository
    {
        void DeleteClient(int clientId);
        int AddClient(Client client);
        IQueryable<Client> GetAllClients();
        Client GetClient(int clientId);
        void UpdateClient(Client client);
    }
}
