using Application.DTO.Assigment;
using Application.DTO.Employee;
using Application.DTO.Report;
using Application.DTO.Service;
using Application.DTO.Specialization;
using Application.Interfaces.Reports;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeservice;

        public EmployeeController(IEmployeeService employeeservice)
        {
            _employeeservice = employeeservice;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDto>> Get()
        {
            var list = _employeeservice.GetAllEmployees();
            return Ok(list);
        }

        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmployeeDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var employee = _employeeservice.GetEmployee(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Create([FromBody] NewEmployeeDto newEmployee)
        {
            if (ModelState.IsValid)
            {
                if (newEmployee.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                var employeeId = _employeeservice.AddEmployee(newEmployee);
                newEmployee.Id = employeeId;
                return CreatedAtRoute("GetEmployee", new { id = employeeId }, newEmployee);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            _employeeservice.DeleteEmployee(id);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] NewEmployeeDto newEmployee)
        {
            if (ModelState.IsValid)
            {
                if (newEmployee.Id > 0)
                {
                    _employeeservice.UpdateEmployee(newEmployee);
                    return NoContent();
                }
            }
            return BadRequest();
        }

        [HttpGet("GetBySpecialization/{specializationId:int}", Name = "GetBySpecialization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<EmployeeDto>> GetBySpecialization(int specializationId)
        {
            if (specializationId == 0)
            {
                return BadRequest();
            }
            var list = _employeeservice.GetAllEmployeesWithSpecialization(specializationId);
            return Ok(list);
        }

        [HttpGet("GetByMainSpecialization/{specializationId:int}", Name = "GetByMainSpecialization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<EmployeeDto>> GetByMainSpecialization(int specializationId)
        {
            if (specializationId == 0)
            {
                return BadRequest();
            }
            var list = _employeeservice.GetAllEmployeesWithMainSpecialization(specializationId);
            return Ok(list);
        }

        [HttpPost("AddSpecialization/{employeeId:int}/{specializationId:int}", Name = "AddSpecialization")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddSpecialization(int employeeId, int specializationId)
        {
            if (employeeId == 0 || specializationId == 0)
            {
                return BadRequest();
            }
            _employeeservice.AddSpecializationToEmployee(employeeId, specializationId);
            return NoContent();
        }

        [HttpPut("UpdateMainSpecialization/{employeeId:int}/{specializationId:int}", Name = "UpdateMainSpecialization")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateMainSpecialization(int employeeId, int specializationId)
        {
            if (employeeId == 0 || specializationId == 0)
            {
                return BadRequest();
            }
            _employeeservice.UpdateEmployeeSpecialization(employeeId, specializationId);
            return NoContent();
        }

        [HttpGet("GetAssigments/{id:int}", Name = "GetAssigments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<AssigmentDto>> GetAssigments(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var list = _employeeservice.GetEmployeeAssigments(id);
            return Ok(list);
        }


        [HttpGet("GetSpecializations/{id:int}", Name = "GetSpecializations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<SpecializationDto>> GetSpecializations(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var list = _employeeservice.GetEmployeeSpecializations(id);
            return Ok(list);
        }
    }
}
