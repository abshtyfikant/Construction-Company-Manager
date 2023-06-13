using Application.DTO.Client;
using Application.DTO.Report;
using Application.DTO.Service;
using Application.Interfaces.Reports;
using Application.Interfaces.Services;
using Domain.Interfaces.Repository;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportservice;

        public ReportController(IReportService reportService)
        {
            _reportservice = reportService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<ServiceForListDto>> Get()
        {
            var list = _reportservice.GetReportsForList();
            return Ok(list);
        }

        [HttpGet("{id:int}", Name = "GetReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ServiceDetailsDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var report = _reportservice.GetReport(id);
            if (report is null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Create([FromBody] NewReportDto newReport)
        {
            if (ModelState.IsValid)
            {
                if (newReport.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                newReport.UserId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var reportId = _reportservice.AddReport(newReport);
                return CreatedAtRoute("GetReport", new { id = reportId }, newReport);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}", Name = "DeleteReport")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            _reportservice.DeleteReport(id);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] NewReportDto newReport)
        {
            if (ModelState.IsValid)
            {
                if (newReport.Id > 0)
                {
                    _reportservice.UpdateReport(newReport);
                    return NoContent();
                }
            }
            return BadRequest();
        }
    }
}
