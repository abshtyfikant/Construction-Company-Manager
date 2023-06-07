using Application.DTO.Client;
using Application.DTO.Report;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class ClientService : IClientService
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

        public object GetClient(int clientId)
        {
            var client = _clientRepo.GetClient(clientId);
            var clientDto = _mapper.Map<ClientDto>(client);
            return clientDto;
        }

        public object GetClientForEdit(int clientId)
        {
            throw new NotImplementedException();
        }

        public List<ClientDto> GetClientsForList()
        {
            var clients = _clientRepo.GetAllClients()
            .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
            .ToList();
            return clients;
        }

        public object UpdateClient(NewClientDto newClient)
        {
            var client = _mapper.Map<Client>(newClient);
            _clientRepo.UpdateClient(client);
            return client;
        }
    }
}
