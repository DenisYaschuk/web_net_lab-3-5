using BBL.DTO;
using BBL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Net;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        private readonly ITeamService _teamService;

        private readonly ILogger<TeamController> _logger;

        public TeamController(ILogger<TeamController> logger, IEmployeeService employeeService, ITeamService teamService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _teamService = teamService;
        }

        [HttpGet("/api/teams/GetTeamById/{teamId}")]
        public async Task<IActionResult> GetById([FromRoute] int teamId)
        {
            var team = await _teamService.GetByIdAsync(teamId);
            if (team == null)
            {
                var error = new ApiError("Team with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(team);
        }

        [HttpGet("/api/teams/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var team = await _teamService.GetAllAsync();
            if (!team.Any())
            {
                var error = new ApiError("No Teams were found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(team);
        }

        [HttpPost("/api/teams/Create")]
        public async Task<IActionResult> Create([FromBody] TeamDTO teamData)
        {
            var team = await _teamService.GetByIdAsync(teamData.Id);

            if(team != null)
            {
                var error = new ApiError("Team with such Id already exists.", HttpStatusCode.BadRequest);
                return BadRequest(error);
            }
            team = await _teamService.CreateAsync(teamData);


            return Ok(team);
        }
        [HttpPut("/api/teams/Update")]
        public async Task<IActionResult> Update([FromBody] TeamDTO teamData)
        {
            var team = await _teamService.GetByIdAsync(teamData.Id);

            if (team == null)
            {
                var error = new ApiError("Team with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }
            if (!await _teamService.UpdateAsync(teamData))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok(teamData);
        }

        [HttpDelete("/api/teams/DeleteById/{teamId}")]
        public async Task<IActionResult> Delete([FromRoute] int teamId)
        {
            var team = await _teamService.GetByIdAsync(teamId);

            if (team == null)
            {
                var error = new ApiError("Team with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            var employees = await _employeeService.GetAllAsync();

            var teamsEmployees = employees.Where(x => x.TeamId == teamId).ToList();

            if(teamsEmployees != null)
            {
                var error = new ApiError("This Team still has employees in it thus it can not be deleted.", HttpStatusCode.BadRequest);
                return BadRequest(error);
            }

            if (!await _teamService.DeleteByIdAsync(teamId))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok();
        }
    }
}
