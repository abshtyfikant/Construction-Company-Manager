using Application.DTO.Client;

namespace Application.Interfaces.Services;

public interface IClientService
{
    int AddClient(NewClientDto client);
    List<ClientDto> GetClientsForList();
    ClientDto GetClient(int clientId);
    NewClientDto UpdateClient(NewClientDto newClient);
    void DeleteClient(int clientId);
}