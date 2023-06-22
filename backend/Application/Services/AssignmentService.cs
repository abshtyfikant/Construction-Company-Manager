using Application.DTO.Assigment;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IMapper _mapper;

    public AssignmentService(IMapper mapper, IAssignmentRepository assignmentRepository)
    {
        _mapper = mapper;
        _assignmentRepository = assignmentRepository;
    }

    public int AddAssignment(NewAssignmentDto assignment)
    {
        var assignmentEntity = _mapper.Map<Assignment>(assignment);
        var id = _assignmentRepository.AddAssignment(assignmentEntity);
        return id;
    }

    public List<AssigmentDto> GetAllAssignments()
    {
        var assignments = _assignmentRepository.GetAllAssignments()
            .ProjectTo<AssigmentDto>(_mapper.ConfigurationProvider)
            .ToList();
        return assignments;
    }

    public AssigmentDto GetAssignment(int assignmentId)
    {
        var assignment = _assignmentRepository.GetAssignment(assignmentId);
        var assignmentDto = _mapper.Map<AssigmentDto>(assignment);
        return assignmentDto;
    }

    public NewAssignmentDto UpdateAssignment(NewAssignmentDto newAssignment)
    {
        var assignmentEntity = _mapper.Map<Assignment>(newAssignment);
        _assignmentRepository.UpdateAssignment(assignmentEntity);
        return newAssignment;
    }

    public void DeleteAssignment(int assignmentId)
    {
        _assignmentRepository.DeleteAssignment(assignmentId);
    }

    public List<AssigmentDto> GetEmployeeAssignments(int employeeId)
    {
        var assignments = _assignmentRepository.GetAllAssignmentsWithEmployee(employeeId)
            .ProjectTo<AssigmentDto>(_mapper.ConfigurationProvider)
            .ToList();
        return assignments;
    }

    public List<AssigmentDto> GetServiceAssignments(int serviceId)
    {
        var assignments = _assignmentRepository.GetAllAssignmentsWithService(serviceId)
            .ProjectTo<AssigmentDto>(_mapper.ConfigurationProvider)
            .ToList();
        return assignments;
    }

}