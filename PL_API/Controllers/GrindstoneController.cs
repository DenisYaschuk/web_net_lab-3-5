using BBL.DTO;
using BBL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Net;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrindstoneController : ControllerBase
    {

        private readonly IGrindstoneService _grindstoneService;
        private readonly IStatusService _statusService;
        private readonly ILogger<TeamController> _logger;

        public GrindstoneController(ILogger<TeamController> logger, IGrindstoneService grindstoneService, IStatusService statusService)
        {
            _logger = logger;
            _grindstoneService = grindstoneService;
            _statusService = statusService;
        }

        [HttpGet("/api/grindstones/GetById/{grindstoneId}")]
        public async Task<IActionResult> GetById([FromRoute] int grindstoneId)
        {
            var grindstone = await _grindstoneService.GetByIdAsync(grindstoneId);
            if (grindstone == null)
            {
                var error = new ApiError("Grindstone with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(grindstone);
        }

        [HttpGet("/api/grindstones/GetAllByStatusId/{statusId}")]
        public async Task<IActionResult> GetAllByStatusId([FromRoute] int statusId)
        {            
            var status = await _statusService.GetByIdAsync(statusId);

            if (status == null)
            {
                var error = new ApiError("Status with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            var grindstones = await _grindstoneService.GetAllAsync();

            var statusGrindstones = grindstones.Where(x => x.StatusId == statusId).ToList();
            if(!statusGrindstones.Any())
            {
                var error = new ApiError("No Grindstones with such Status were found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }
            return Ok(statusGrindstones);
        }

        [HttpGet("/api/grindstones/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var grindstones = await _grindstoneService.GetAllAsync();
            if (!grindstones.Any())
            {
                var error = new ApiError("No Grindstones were found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(grindstones);
        }


        [HttpPost("/api/grindstones/Create")]
        public async Task<IActionResult> Create([FromBody] GrindstoneDTO grindstoneData)
        {
            var team = await _grindstoneService.GetByIdAsync(grindstoneData.Id);

            if (team != null)
            {
                var error = new ApiError("Grindstone with such Id already exists.", HttpStatusCode.BadRequest);
                return BadRequest(error);
            }
            team = await _grindstoneService.CreateAsync(grindstoneData);


            return Ok(team);
        }
        [HttpPut("/api/grindstones/Update")]
        public async Task<IActionResult> Update([FromBody] GrindstoneDTO grindstoneData)
        {
            var grindstone = await _grindstoneService.GetByIdAsync(grindstoneData.Id);

            if (grindstone == null)
            {
                var error = new ApiError("Grindstone with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }
            if (!await _grindstoneService.UpdateAsync(grindstoneData))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok(grindstoneData);
        }

        [HttpDelete("/api/grindstones/DeleteById/{grindstoneId}")]
        public async Task<IActionResult> Delete([FromRoute] int grindstoneId)
        {
            var grindstone = await _grindstoneService.GetByIdAsync(grindstoneId);

            if (grindstone == null)
            {
                var error = new ApiError("Grindstone with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            if (!await _grindstoneService.DeleteByIdAsync(grindstoneId))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok();
        }
    }
}
