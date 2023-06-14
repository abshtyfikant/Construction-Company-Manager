﻿using Application.DTO.Assigment;
using Application.DTO.Employee;
using Application.DTO.Specialization;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        void DeleteEmployee(int employeeId); //
        int AddEmployee(NewEmployeeDto employee); //
        List<EmployeeDto> GetAllEmployees(); //
        List<EmployeeDto> GetAllEmployeesWithSpecialization(int specializationId); //
        List<EmployeeDto> GetAllEmployeesWithMainSpecialization(int specializationId); //
        object GetEmployee(int employeeId); //
        void AddSpecializationToEmployee(int employeeId, int specializationId); // 
        object UpdateEmployee(NewEmployeeDto employee); //
        void UpdateEmployeeSpecialization(int employeeId, int specializationId);//  
        List<AssigmentDto> GetEmployeeAssigments(int employeeId);
        List<SpecializationDto> GetEmployeeSpecializations(int employeeId);
    }
}