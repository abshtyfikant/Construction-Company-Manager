using Application.DTO.Assigment;
using Application.DTO.Employee;
using Application.DTO.Specialization;

namespace Application.Interfaces.Services;

public interface IEmployeeService
{
    void DeleteEmployee(int employeeId);
    int AddEmployee(NewEmployeeDto employee);
    List<EmployeeDto> GetAllEmployees();
    List<EmployeeDto> GetAllEmployeesWithSpecialization(int specializationId);
    List<EmployeeDto> GetAllEmployeesWithMainSpecialization(int specializationId);
    List<EmployeeDto> GetAllEmployeesWithSpecializationAnyKind(int specializationId);
    object GetEmployee(int employeeId);
    void AddSpecializationToEmployee(int employeeId, int specializationId);
    object UpdateEmployee(NewEmployeeDto employee);
    void UpdateEmployeeSpecialization(int employeeId, int specializationId);
    List<AssigmentDto> GetEmployeeAssignments(int employeeId);
    List<SpecializationDto> GetEmployeeSpecializations(int employeeId);
    List<EmployeeDto> GetAvailableEmployeesForTime(DateTime startTime, DateTime endTime);
    double GetEmployeeEarnings(DateTime start, DateTime end, int employeeId);
    double GetEmployeesEarnings(DateTime start, DateTime end);
}
