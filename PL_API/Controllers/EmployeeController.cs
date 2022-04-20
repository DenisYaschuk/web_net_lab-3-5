using BBL.DTO;
using BBL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Net;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        private readonly ITeamService _teamService;

        private readonly ILogger<TeamController> _logger;

        public EmployeeController(ILogger<TeamController> logger, IEmployeeService employeeService, ITeamService teamService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _teamService = teamService;
        }

        [HttpGet("/api/employees/GetById/{employeeId}")]
        public async Task<IActionResult> GetById([FromRoute] int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
            {
                var error = new ApiError("Employee with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(employee);
        }

        [HttpGet("/api/employees/GetAllByTeamId/{teamId}")]
        public async Task<IActionResult> GetAllEmployeesByTeamId([FromRoute] int teamId)
        {
            var team = await _teamService.GetByIdAsync(teamId);

            if (team == null)
            {
                var error = new ApiError("Team with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            var employees = await _employeeService.GetAllAsync();

            var teamsEmployees = employees.Where(x => x.TeamId == teamId).ToList();

            if (!teamsEmployees.Any())
            {
                var error = new ApiError("This Team has no employees.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(teamsEmployees);
        }

        [HttpGet("/api/employees/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            if (!employees.Any())
            {
                var error = new ApiError("No Employees were found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            return Ok(employees);
        }

        [HttpPost("/api/employees/Create")]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO employeeData)
        {
            var employee = await _employeeService.GetByIdAsync(employeeData.Id);

            if (employee != null)
            {
                var error = new ApiError("Employee with such Id already exists.", HttpStatusCode.BadRequest);
                return BadRequest(error);
            }

            var team = await _teamService.GetByIdAsync(employeeData.TeamId);

            if (team == null)
            {
                var error = new ApiError("Team with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }


            employee = await _employeeService.CreateAsync(employeeData);


            return Ok(employee);
        }
        [HttpPut("/api/employees/Update")]
        public async Task<IActionResult> Update([FromBody] EmployeeDTO employeeData)
        {
            var employee = await _employeeService.GetByIdAsync(employeeData.Id);

            if (employee == null)
            {
                var error = new ApiError("Team with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            var team = await _teamService.GetByIdAsync(employeeData.TeamId);

            if (team == null)
            {
                var error = new ApiError("Team with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            if (!await _employeeService.UpdateAsync(employeeData))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok(employeeData);
        }

        [HttpDelete("/api/employees/DeleteById/{employeeId}")]
        public async Task<IActionResult> Delete([FromRoute] int employeeId)
        {
            var team = await _employeeService.GetByIdAsync(employeeId);

            if (team == null)
            {
                var error = new ApiError("Team with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            if (!await _employeeService.DeleteByIdAsync(employeeId))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok();
        }
    }
}
