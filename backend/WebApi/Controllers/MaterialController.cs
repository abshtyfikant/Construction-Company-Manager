using Application.DTO.Material;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService _materialservice;

    public MaterialController(IMaterialService materialservice)
    {
        _materialservice = materialservice;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<MaterialDto>> Get()
    {
        var list = _materialservice.GetMaterialsForList();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetMaterial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MaterialDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var material = _materialservice.GetMaterial(id);
        if (material is null) return NotFound();
        return Ok(material);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewMaterialDto newMaterial)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newMaterial.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        var id = _materialservice.AddMaterial(newMaterial);
        newMaterial.Id = id;
        return CreatedAtRoute("GetMaterial", new { id }, newMaterial);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Update([FromBody] NewMaterialDto material)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (material.Id <= 0) return BadRequest();
        _materialservice.UpdateMaterial(material);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest();
        _materialservice.DeleteMaterial(id);
        return NoContent();
    }

    [HttpGet("GetByService/{specializationId:int}", Name = "GetByService")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<MaterialDto>> GetByService(int specializationId)
    {
        if (specializationId == 0) return BadRequest();
        var list = _materialservice.GetMaterialsByServiceForList(specializationId);
        return Ok(list);
    }

    [HttpGet("GetCostInTime/{begin:datetime}/{end:datetime}")]
    public ActionResult<double> GetCostInTime(DateTime start, DateTime end)
    {
        if (start == null || end == null || start > end) return BadRequest();
        var cost = _materialservice.GetTotalCostInTime(start, end);
        return Ok(cost);
    }
}
