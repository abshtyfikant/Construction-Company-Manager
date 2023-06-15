using Application.DTO.Assigment;
using Application.DTO.Employee;
using Application.DTO.Specialization;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<EmployeeDto>> Get()
    {
        var list = _employeeService.GetAllEmployees();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<EmployeeDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var employee = _employeeService.GetEmployee(id);
        if (employee is null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewEmployeeDto newEmployee)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newEmployee.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        var employeeId = _employeeService.AddEmployee(newEmployee);
        newEmployee.Id = employeeId;
        return CreatedAtRoute("GetEmployee", new { id = employeeId }, newEmployee);
    }

    [HttpDelete("{id:int}", Name = "DeleteEmployee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest();
        _employeeService.DeleteEmployee(id);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] NewEmployeeDto newEmployee)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newEmployee.Id <= 0) return BadRequest();
        _employeeService.UpdateEmployee(newEmployee);
        return NoContent();
    }

    [HttpGet($"{nameof(GetBySpecialization)}/{{specializationId:int}}", Name = "GetBySpecialization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<EmployeeDto>> GetBySpecialization(int specializationId)
    {
        if (specializationId == 0) return BadRequest();
        var list = _employeeService.GetAllEmployeesWithSpecialization(specializationId);
        return Ok(list);
    }

    [HttpGet($"{nameof(GetByMainSpecialization)}/{{specializationId:int}}", Name = "GetByMainSpecialization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<EmployeeDto>> GetByMainSpecialization(int specializationId)
    {
        if (specializationId == 0) return BadRequest();
        var list = _employeeService.GetAllEmployeesWithMainSpecialization(specializationId);
        return Ok(list);
    }

    [HttpPost($"{nameof(AddSpecialization)}/{{employeeId:int}}/{{specializationId:int}}", Name = "AddSpecialization")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddSpecialization(int employeeId, int specializationId)
    {
        if (employeeId == 0 || specializationId == 0) return BadRequest();
        _employeeService.AddSpecializationToEmployee(employeeId, specializationId);
        return NoContent();
    }

    [HttpPut($"{nameof(UpdateMainSpecialization)}/{{employeeId:int}}/{{specializationId:int}}", Name = "UpdateMainSpecialization")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateMainSpecialization(int employeeId, int specializationId)
    {
        if (employeeId == 0 || specializationId == 0) return BadRequest();
        _employeeService.UpdateEmployeeSpecialization(employeeId, specializationId);
        return NoContent();
    }

    [HttpGet($"{nameof(GetAssignments)}/{{id:int}}", Name = "GetAssignments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<AssigmentDto>> GetAssignments(int id)
    {
        if (id == 0) return BadRequest();
        var list = _employeeService.GetEmployeeAssigments(id);
        return Ok(list);
    }


    [HttpGet($"{nameof(GetSpecializations)}/{{id:int}}", Name = "GetSpecializations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<SpecializationDto>> GetSpecializations(int id)
    {
        if (id == 0) return BadRequest();
        var list = _employeeService.GetEmployeeSpecializations(id);
        return Ok(list);
    }
}