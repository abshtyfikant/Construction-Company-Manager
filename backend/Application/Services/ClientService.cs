using Application.DTO.Client;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepo;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepo, IMapper mapper)
    {
        _clientRepo = clientRepo;
        _mapper = mapper;
    }


    public int AddClient(NewClientDto client)
    {
        var cli = _mapper.Map<Client>(client);
        var id = _clientRepo.AddClient(cli);
        return id;
    }

    public void DeleteClient(int clientId)
    {
        _clientRepo.DeleteClient(clientId);
    }

    public ClientDto GetClient(int clientId)
    {
        var client = _clientRepo.GetClient(clientId);
        var clientDto = _mapper.Map<ClientDto>(client);
        return clientDto;
    }

    public List<ClientDto> GetClientsForList()
    {
        var clients = _clientRepo.GetAllClients()
            .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
            .ToList();
        return clients;
    }

    public NewClientDto UpdateClient(NewClientDto newClient)
    {
        var client = _mapper.Map<Client>(newClient);
        _clientRepo.UpdateClient(client);
        return newClient;
    }
}