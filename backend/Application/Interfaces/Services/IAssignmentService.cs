using Application.DTO.Assigment;

namespace Application.Interfaces.Services;

public interface IAssignmentService
{
    int AddAssignment(NewAssignmentDto assignment);
    List<AssigmentDto> GetAllAssignments();
    AssigmentDto GetAssignment(int assignmentId);
    NewAssignmentDto UpdateAssignment(NewAssignmentDto newAssignment);
    void DeleteAssignment(int assignmentId);
    List<AssigmentDto> GetEmployeeAssignments(int employeeId);
    List<AssigmentDto> GetServiceAssignments(int serviceId);
}