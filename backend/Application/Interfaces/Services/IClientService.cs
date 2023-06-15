using Application.DTO.Client;

namespace Application.Interfaces.Services;

public interface IClientService
{
    int AddClient(NewClientDto client);
    List<ClientDto> GetClientsForList();
    object GetClient(int clientId);
    object UpdateClient(NewClientDto newClient);
    void DeleteClient(int clientId);
}