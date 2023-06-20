using Application.DTO.Client;
using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Model;
using Moq;

namespace Tests;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly IMapper _mapper;

    public ClientServiceTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public void AddClient_ShouldReturnId()
    {
        // Arrane
        var clientService = new ClientService(_clientRepositoryMock.Object, _mapper);
        var client = new NewClientDto
        {
            Id = 1,
            FirstName = "test",
            LastName = "test",
            City = "test"
        };
        _clientRepositoryMock.Setup(x => x.AddClient(It.IsAny<Client>())).Returns(1);

        // Act
        var result = clientService.AddClient(client);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void DeleteClient_ShouldBeCalledOnce()
    {
        // Arrange
        var clientService = new ClientService(_clientRepositoryMock.Object, _mapper);
        var clientId = 1;

        // Act
        clientService.DeleteClient(clientId);

        // Assert
        _clientRepositoryMock.Verify(x => x.DeleteClient(clientId), Times.Once);
    }

    [Fact]
    public void GetClient_ShouldReturnClient()
    {
        // Arrange
        var clientService = new ClientService(_clientRepositoryMock.Object, _mapper);
        var client = new Client
        {
            Id = 1,
            FirstName = "test",
            LastName = "test",
            City = "test"
        };
        _clientRepositoryMock.Setup(x => x.GetClient(It.IsAny<int>())).Returns(client);

        // Act
        var result = clientService.GetClient(1);

        // Assert
        Assert.Equal(client.Id, result.Id);
    }

    [Fact]
    public void GetClientsForList_ShouldReturnListOfClients()
    {
        // Arrange
        var clientService = new ClientService(_clientRepositoryMock.Object, _mapper);
        var clients = new List<Client>
        {
            new()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                City = "test"
            },
            new()
            {
                Id = 2,
                FirstName = "test",
                LastName = "test",
                City = "test"
            }
        };
        _clientRepositoryMock.Setup(x => x.GetAllClients()).Returns(clients.AsQueryable());

        // Act
        var result = clientService.GetClientsForList();

        // Assert
        Assert.Equal(clients.Count, result.Count);
    }

    [Fact]
    public void UpdateClient_ShouldReturnClient()
    {
        // Arrange
        var clientService = new ClientService(_clientRepositoryMock.Object, _mapper);
        var client = new NewClientDto
        {
            Id = 1,
            FirstName = "test",
            LastName = "test",
            City = "test"
        };

        // Act
        var result = clientService.UpdateClient(client);

        // Assert
        Assert.Equal(client.Id, result.Id);
    }
}