using Application.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IClientService
    {
        int AddClient(NewClientDto client);
        List<ClientDto> GetClientsForList();
        object GetClient(int clientId);
        object GetClientForEdit(int clientId);
        object UpdateClient(NewClientDto newClient);
        void DeleteClient(int clientId);
    }
}
