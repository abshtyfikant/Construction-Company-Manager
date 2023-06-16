using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int AddEmployee(Employee employee)
    {
        if (!_dbContext.Specializations.Any(s => s.Id == employee.MainSpecializationId))
            throw new Exception("Specialization not found");

        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();
        return employee.Id;
    }

    public void AddSpecializationToEmployee(int employeeId, int specializationId)
    {
        if (!_dbContext.Specializations.Any(s => s.Id == specializationId))
            throw new Exception("Specialization not found");

        if (!_dbContext.Employees.Any(s => s.Id == employeeId)) throw new Exception("Employee not found");

        var employeeSpecialization = new EmployeeSpecialization
        {
            EmployeeId = employeeId,
            SpecializationId = specializationId
        };
        _dbContext.SpecializationAssignments.Add(employeeSpecialization);
        _dbContext.SaveChanges();
    }

    public void DeleteEmployee(int employeeId)
    {
        var assignments = _dbContext.Assignments.Where(i => i.EmployeeId == employeeId);
        if (assignments is not null)
        {
            throw new Exception("Employee is assigned to service");
        }
        var employee = _dbContext.Employees.Find(employeeId);
        if (employee is not null)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }
    }

    public IQueryable<Employee> GetAllEmployees()
    {
        var employees = _dbContext.Employees;
        return employees;
    }

    public IQueryable<Employee> GetAllEmployeesWithMainSpecialization(int specializationId)
    {
        if (!_dbContext.Specializations.Any(s => s.Id == specializationId))
            throw new Exception("Specialization not found");

        var employees = _dbContext.Specializations
            .Where(i => i.Id == specializationId)
            .SelectMany(i => i.MainSpecializationEmployees);

        return employees;
    }

    public IQueryable<Employee> GetAllEmployeesWithSpecialization(int specializationId)
    {
        if (!_dbContext.Specializations.Any(s => s.Id == specializationId))
            throw new Exception("Specialization not found");

        var employees = _dbContext.Specializations
            .Where(i => i.Id == specializationId)
            .SelectMany(i => i.Employees);

        return employees;
    }

    public Employee GetEmployee(int employeeId)
    {
        var employee = _dbContext.Employees.FirstOrDefault(i => i.Id == employeeId);
        return employee;
    }

    public IQueryable<Assignment> GetEmployeeAssigments(int employeeId)
    {
        if (!_dbContext.Employees.Any(s => s.Id == employeeId)) throw new Exception("Employee not found");

        var assigments = _dbContext.Assignments.Where(i => i.EmployeeId == employeeId);
        return assigments;
    }

    public IQueryable<Specialization> GetEmployeeSpecializations(int employeeId)
    {
        if (!_dbContext.Employees.Any(s => s.Id == employeeId)) throw new Exception("Employee not found");

        var specializations = _dbContext.Employees
            .Where(i => i.Id == employeeId)
            .SelectMany(i => i.Specializations);

        return specializations;
    }

    public void UpdateEmployee(Employee employee)
    {
        if (!_dbContext.Specializations.Any(s => s.Id == employee.MainSpecializationId))
            throw new Exception("Specialization not found");

        _dbContext.Attach(employee);
        _dbContext.Entry(employee).Property("FirstName").IsModified = true;
        _dbContext.Entry(employee).Property("LastName").IsModified = true;
        _dbContext.Entry(employee).Property("City").IsModified = true;
        _dbContext.Entry(employee).Property("RatePerHour").IsModified = true;
        _dbContext.SaveChanges();
    }

    public void UpdateEmployeeSpecialization(int employeeId, int specializationId)
    {
        if (!_dbContext.Specializations.Any(s => s.Id == specializationId))
            throw new Exception("Specialization not found");

        if (!_dbContext.Employees.Any(s => s.Id == employeeId))
            throw new Exception("Employee not found");

        var employee = _dbContext.Employees.Find(employeeId);
        if (employee is not null)
        {
            _dbContext.Attach(employee);
            _dbContext.Entry(employee).Property("MainSpecializationId").IsModified = true;
            _dbContext.SaveChanges();
        }
    }
}