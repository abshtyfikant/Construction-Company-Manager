using Application.DTO.Assigment;
using Application.DTO.Employee;
using Application.DTO.Specialization;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public int AddEmployee(NewEmployeeDto employee)
    {
        var employeeEntity = _mapper.Map<Employee>(employee);
        var id = _employeeRepository.AddEmployee(employeeEntity);
        return id;
    }

    public void AddSpecializationToEmployee(int employeeId, int specializationId)
    {
        _employeeRepository.AddSpecializationToEmployee(employeeId, specializationId);
    }

    public void DeleteEmployee(int employeeId)
    {
        _employeeRepository.DeleteEmployee(employeeId);
    }

    public List<EmployeeDto> GetAllEmployees()
    {
        var employees = _employeeRepository.GetAllEmployees()
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .ToList();
        return employees;
    }

    public List<EmployeeDto> GetAllEmployeesWithMainSpecialization(int specializationId)
    {
        var employees = _employeeRepository.GetAllEmployeesWithMainSpecialization(specializationId)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .ToList();
        return employees;
    }

    public List<EmployeeDto> GetAllEmployeesWithSpecializationAnyKind(int specializationId)
    {
        var employeesMainSpec = GetAllEmployeesWithMainSpecialization(specializationId);
        var employeesOtherSpec = GetAllEmployeesWithSpecialization(specializationId);
        var employees = employeesMainSpec.Union(employeesOtherSpec).ToList();
        return employees;
    }

    public List<EmployeeDto> GetAllEmployeesWithSpecialization(int specializationId)
    {
        var employees = _employeeRepository.GetAllEmployeesWithSpecialization(specializationId)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .ToList();
        return employees;
    }

    public object GetEmployee(int employeeId)
    {
        var employee = _employeeRepository.GetEmployee(employeeId);
        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public List<AssigmentDto> GetEmployeeAssignments(int employeeId)
    {
        var assigments = _employeeRepository.GetEmployeeAssignments(employeeId)
            .ProjectTo<AssigmentDto>(_mapper.ConfigurationProvider)
            .ToList();
        return assigments;
    }

    public List<SpecializationDto> GetEmployeeSpecializations(int employeeId)
    {
        var specializations = _employeeRepository.GetEmployeeSpecializations(employeeId)
            .ProjectTo<SpecializationDto>(_mapper.ConfigurationProvider)
            .ToList();
        return specializations;
    }

    public object UpdateEmployee(NewEmployeeDto employee)
    {
        var employeeEntity = _mapper.Map<Employee>(employee);
        _employeeRepository.UpdateEmployee(employeeEntity);
        return employeeEntity;
    }

    public void UpdateEmployeeSpecialization(int employeeId, int specializationId)
    {
        _employeeRepository.UpdateEmployeeSpecialization(employeeId, specializationId);
    }

    public List<EmployeeDto> GetAvailableEmployeesForTime(DateTime startTime, DateTime endTime)
    {
        var employees = _employeeRepository.GetAvailableEmployeesForTime(startTime, endTime)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .ToList();
        return employees;
    }

    public double GetEmployeeEarnings(DateTime start, DateTime end, int employeeId)
    {
        return _employeeRepository.GetEmployeeEarnings(start, end, employeeId);
    }

    public double GetEmployeesEarnings(DateTime start, DateTime end)
    {
        return _employeeRepository.GetEmployeesEarnings(start, end);
    }
}
