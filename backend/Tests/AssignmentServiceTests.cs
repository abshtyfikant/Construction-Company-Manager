using Application.DTO.Assigment;
using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Model;
using Moq;

namespace Tests;

public class AssignmentServiceTests
{
    private readonly Mock<IAssignmentRepository> _assignmentRepositoryMock;
    private readonly IMapper _mapper;

    public AssignmentServiceTests()
    {
        _assignmentRepositoryMock = new Mock<IAssignmentRepository>();
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public void AddAssignment_ShouldReturnId()
    {
        // Arrane
        var assignmentService = new AssignmentService(_mapper, _assignmentRepositoryMock.Object);
        var assignment = new NewAssignmentDto
        {
            Id = 1,
            EmployeeId = 1,
            ServiceId = 1,
            Function = "test"
        };
        _assignmentRepositoryMock.Setup(x => x.AddAssignment(It.IsAny<Assignment>())).Returns(1);

        // Act
        var result = assignmentService.AddAssignment(assignment);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void GetAllAssignments_ShouldReturnListOfAssignments()
    {
        // Arrange
        var assignmentService = new AssignmentService(_mapper, _assignmentRepositoryMock.Object);
        var assignments = new List<Assignment>
        {
            new()
            {
                Id = 1,
                EmployeeId = 1,
                Employee = new Employee(),
                ServiceId = 1,
                Service = new Service(),
                Function = "test"
            },
            new()
            {
                Id = 2,
                EmployeeId = 2,
                Employee = new Employee(),
                ServiceId = 2,
                Service = new Service(),
                Function = "test2"
            }
        };
        var queryableAssignments = assignments.AsQueryable();
        _assignmentRepositoryMock.Setup(x => x.GetAllAssignments()).Returns(queryableAssignments);

        // Act
        var result = assignmentService.GetAllAssignments();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetAssignment_ShouldReturnAssignment()
    {
        // Arrange
        var assignmentService = new AssignmentService(_mapper, _assignmentRepositoryMock.Object);
        var assignment = new Assignment
        {
            Id = 1,
            EmployeeId = 1,
            Employee = new Employee(),
            ServiceId = 1,
            Service = new Service(),
            Function = "test"
        };
        _assignmentRepositoryMock.Setup(x => x.GetAssignment(It.IsAny<int>())).Returns(assignment);

        // Act
        var result = assignmentService.GetAssignment(1);

        // Assert
        Assert.Equal(assignment.Id, result.Id);
    }

    [Fact]
    public void UpdateAssignment_ShouldReturnAssignment()
    {
        // Arrange
        var assignmentService = new AssignmentService(_mapper, _assignmentRepositoryMock.Object);
        var assignment = new NewAssignmentDto
        {
            Id = 1,
            EmployeeId = 1,
            ServiceId = 1,
            Function = "test"
        };

        // Act
        var result = assignmentService.UpdateAssignment(assignment);

        // Assert
        Assert.Equal(assignment.Id, result.Id);
    }

    [Fact]
    public void DeleteAssignment_ShouldCallDeleteAssignmentOnce()
    {
        // Arrange
        var assignmentService = new AssignmentService(_mapper, _assignmentRepositoryMock.Object);

        // Act
        assignmentService.DeleteAssignment(1);

        // Assert
        _assignmentRepositoryMock.Verify(x => x.DeleteAssignment(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void GetEmployeeAssignments_ShouldReturnListOfAssignments()
    {
        // Arrange
        var assignmentService = new AssignmentService(_mapper, _assignmentRepositoryMock.Object);
        var assignments = new List<Assignment>
        {
            new()
            {
                Id = 1,
                EmployeeId = 1,
                Employee = new Employee(),
                ServiceId = 1,
                Service = new Service(),
                Function = "test"
            },
            new()
            {
                Id = 2,
                EmployeeId = 1,
                Employee = new Employee(),
                ServiceId = 2,
                Service = new Service(),
                Function = "test2"
            }
        };
        var queryableAssignments = assignments.AsQueryable();
        _assignmentRepositoryMock.Setup(x => x.GetAllAssignmentsWithEmployee(It.IsAny<int>()))
            .Returns(queryableAssignments);

        // Act
        var result = assignmentService.GetEmployeeAssignments(1);

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetServiceAssignments_ShouldReturnListOfAssignments()
    {
        // Arrange
        var assignmentService = new AssignmentService(_mapper, _assignmentRepositoryMock.Object);
        var assignments = new List<Assignment>
        {
            new()
            {
                Id = 1,
                EmployeeId = 1,
                Employee = new Employee(),
                ServiceId = 1,
                Service = new Service(),
                Function = "test"
            },
            new()
            {
                Id = 2,
                EmployeeId = 2,
                Employee = new Employee(),
                ServiceId = 1,
                Service = new Service(),
                Function = "test2"
            }
        };
        var queryableAssignments = assignments.AsQueryable();
        _assignmentRepositoryMock.Setup(x => x.GetAllAssignmentsWithService(It.IsAny<int>()))
            .Returns(queryableAssignments);

        // Act
        var result = assignmentService.GetServiceAssignments(1);

        // Assert
        Assert.Equal(2, result.Count);
    }
}