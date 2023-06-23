using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AssignmentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void DeleteAssignment(int id)
    {
        var assignment = _dbContext.Assignments.Find(id);
        if (assignment is not null)
        {
            _dbContext.Assignments.Remove(assignment);
            _dbContext.SaveChanges();
        }
    }

    public int AddAssignment(Assignment assignment)
    {
        if (!_dbContext.Employees.Any(e => e.Id == assignment.EmployeeId))
            throw new Exception("Employee not found");
        if (!_dbContext.Services.Any(s => s.Id == assignment.ServiceId))
            throw new Exception("Service not found");

        _dbContext.Assignments.Add(assignment);
        _dbContext.SaveChanges();
        return assignment.Id;
    }

    public IQueryable<Assignment> GetAllAssignments()
    {
        var assignments = _dbContext.Assignments;
        return assignments;
    }

    public IQueryable<Assignment> GetAllAssignmentsWithEmployee(int employeeId)
    {
        if (!_dbContext.Employees.Any(e => e.Id == employeeId))
            throw new Exception("Employee not found");
        var assignments = _dbContext.Assignments.Where(a => a.EmployeeId == employeeId);
        return assignments;
    }

    public IQueryable<Assignment> GetAllAssignmentsWithService(int serviceId)
    {
        if (!_dbContext.Services.Any(s => s.Id == serviceId))
            throw new Exception("Service not found");
        var assignments = _dbContext.Assignments.Where(a => a.ServiceId == serviceId);
        return assignments;
    }

    public Assignment? GetAssignment(int id)
    {
        var assignment = _dbContext.Assignments.FirstOrDefault(i => i.Id == id);
        return assignment;
    }

    public void UpdateAssignment(Assignment assignment)
    {
        if (!_dbContext.Assignments.Any(a => a.Id == assignment.Id))
            throw new Exception("Assignment not found");
        if (!_dbContext.Employees.Any(e => e.Id == assignment.EmployeeId))
            throw new Exception("Employee not found");
        if (!_dbContext.Services.Any(s => s.Id == assignment.ServiceId))
            throw new Exception("Service not found");

        var assignmentToUpdate = _dbContext.Assignments.Find(assignment.Id);
        if (assignmentToUpdate != null)
        {
            assignmentToUpdate.Function = assignment.Function;
            assignmentToUpdate.EmployeeId = assignment.EmployeeId;
            assignmentToUpdate.ServiceId = assignment.ServiceId;
            _dbContext.SaveChanges();
        }
    }
}