using Application.DTO.Report;
using Application.DTO.Service;
using Application.Interfaces.Reports;
using Application.Interfaces.Services;
using Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
            var model = _reportservice.GetReportsForList();
            return Ok(model);
        }

        [HttpGet("{id:int}")]
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
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
                _reportservice.AddReport(newReport);
                return Ok();
            }
            return BadRequest();
        }
    }
}
