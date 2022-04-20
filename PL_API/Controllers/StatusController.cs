using BBL.DTO;
using BBL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Net;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {

        private readonly IStatusService _statusService;

        private readonly ILogger<TeamController> _logger;

        public StatusController(ILogger<TeamController> logger, IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }

        [HttpGet("/api/statuses/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _statusService.GetAllAsync();
            if (!statuses.Any())
            {
                var error = new ApiError("No Statuses were not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(statuses);
        }

        [HttpGet("/api/statuses/GetById/{statusId}")]
        public async Task<IActionResult> GetById([FromRoute] int statusId)
        {
            var status = await _statusService.GetByIdAsync(statusId);
            if (status == null)
            {
                var error = new ApiError("Status with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(status);
        }
    }
}
