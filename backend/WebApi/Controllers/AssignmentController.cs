using Application.DTO.Assigment;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AssignmentController : ControllerBase
{
    private readonly IAssignmentService _assignmentService;

    public AssignmentController(IAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<AssigmentDto>> Get()
    {
        var list = _assignmentService.GetAllAssignments();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetAssignment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AssigmentDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var assignment = _assignmentService.GetAssignment(id);
        if (assignment is null) return NotFound();
        return Ok(assignment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewAssignmentDto newAssignment)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newAssignment.EndDate > newAssignment.StartDate) return BadRequest();
        if (newAssignment.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        var assignmentId = _assignmentService.AddAssignment(newAssignment);
        newAssignment.Id = assignmentId;
        return CreatedAtRoute("GetAssignment", new { id = assignmentId }, newAssignment);
    }

    [HttpDelete("{id:int}", Name = "DeleteAssignment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest();
        _assignmentService.DeleteAssignment(id);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateAssignment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(int id, [FromBody] NewAssignmentDto newAssignment)
    {
        if (id <= 0) return BadRequest();
        if (newAssignment.EndDate > newAssignment.StartDate) return BadRequest();
        if (!ModelState.IsValid) return BadRequest();
        _assignmentService.UpdateAssignment(newAssignment);
        return NoContent();
    }

    [HttpGet($"{nameof(GetEmployeeAssignments)}/{{employeeId:int}}", Name = "GetEmployeeAssignments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<AssigmentDto>> GetEmployeeAssignments(int employeeId)
    {
        if (employeeId == 0) return BadRequest();
        var list = _assignmentService.GetEmployeeAssignments(employeeId);
        return Ok(list);
    }

    [HttpGet($"{nameof(GetServiceAssignments)}/{{serviceId:int}}", Name = "GetServiceAssignments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<AssigmentDto>> GetServiceAssignments(int serviceId)
    {
        if (serviceId == 0) return BadRequest();
        var list = _assignmentService.GetServiceAssignments(serviceId);
        return Ok(list);
    }
}