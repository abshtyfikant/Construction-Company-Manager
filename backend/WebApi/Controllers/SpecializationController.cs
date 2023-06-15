using Application.DTO.Specialization;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<SpecializationDto>> Get()
    {
        var list = _specializationService.GetSpecializationsForList();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetSpecialization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SpecializationDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var specialization = _specializationService.GetSpecialization(id);
        if (specialization is not null) return NotFound();
        return Ok(specialization);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewSpecializationDto newSpecialization)
    {
        if (ModelState.IsValid)
        {
            if (newSpecialization.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
            var specializationId = _specializationService.AddSpecialization(newSpecialization);
            newSpecialization.Id = specializationId;
            return CreatedAtRoute("GetSpecialization", new { id = specializationId }, newSpecialization);
        }

        return BadRequest();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest();
        _specializationService.DeleteSpecialization(id);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] NewSpecializationDto newSpecialization)
    {
        if (ModelState.IsValid)
            if (newSpecialization.Id > 0)
            {
                _specializationService.UpdateSpecialization(newSpecialization);
                return NoContent();
            }

        return BadRequest();
    }
}