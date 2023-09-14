using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IEmployeeRepository
{
    void DeleteEmployee(int employeeId);
    int AddEmployee(Employee employee);
    IQueryable<Employee> GetAllEmployees();
    IQueryable<Employee> GetAllEmployeesWithSpecialization(int specializationId);
    IQueryable<Employee> GetAllEmployeesWithMainSpecialization(int specializationId);
    Employee GetEmployee(int employeeId);
    void AddSpecializationToEmployee(int employeeId, int specializationId);
    void UpdateEmployee(Employee employee);
    void UpdateEmployeeSpecialization(int employeeId, int specializationId);
    IQueryable<Assignment> GetEmployeeAssignments(int employeeId);
    IQueryable<Specialization> GetEmployeeSpecializations(int employeeId);
    IQueryable<Employee> GetAvailableEmployeesForTime(DateTime start, DateTime end);
    double GetEmployeeEarnings(DateTime start, DateTime end, int employeeId);
    double GetEmployeesEarnings(DateTime start, DateTime end);
}
