using Application.DTO.Service;
using Application.Interfaces.Reports;
using Application.Interfaces.Services;
using Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<ServiceForListDto>> Get()
        {
            var model = _reportservice.GetReportsForList();
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
    }
}
