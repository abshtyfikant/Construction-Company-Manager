using System.Security.Claims;
using Application.DTO.Report;
using Application.DTO.Service;
using Application.Interfaces.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ReportForListDto>> Get()
    {
        var list = _reportService.GetReportsForList();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ReportDetailsDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var report = _reportService.GetReport(id);
        if (report is null) return NotFound();
        return Ok(report);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewReportDto newReport)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newReport.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        newReport.UserId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var reportId = _reportService.AddReport(newReport);
        newReport.Id = reportId;
        return CreatedAtRoute("GetReport", new { id = reportId }, newReport);
    }

    [HttpDelete("{id:int}", Name = "DeleteReport")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest();
        _reportService.DeleteReport(id);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] NewReportDto newReport)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newReport.Id <= 0) return BadRequest();
        _reportService.UpdateReport(newReport);
        return NoContent();
    }

}