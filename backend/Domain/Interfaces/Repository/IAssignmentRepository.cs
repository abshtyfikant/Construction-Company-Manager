using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IAssignmentRepository
{
    void DeleteAssignment(int id);
    int AddAssignment(Assignment assignment);
    IQueryable<Assignment> GetAllAssignments();
    IQueryable<Assignment> GetAllAssignmentsWithEmployee(int employeeId);
    IQueryable<Assignment> GetAllAssignmentsWithService(int serviceId);
    Assignment? GetAssignment(int id);
    void UpdateAssignment(Assignment assignment);
}