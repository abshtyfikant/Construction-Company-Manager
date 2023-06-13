using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
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
        IQueryable<Assigment> GetEmployeeAssigments(int employeeId);
        IQueryable<Specialization> GetEmployeeSpecializations(int employeeId);
    }
}
