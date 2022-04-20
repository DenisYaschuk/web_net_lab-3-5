using BBL.DTO;
using BBL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Net;

namespace PL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrindstoneEmployeeController : ControllerBase
    {

        private readonly IGrindstoneService _grindstoneService;
        private readonly IEmployeeService _employeeService;
        private readonly IGrindstoneEmployeeService _grindstoneEmployeeService;
        private readonly ILogger<TeamController> _logger;

        public GrindstoneEmployeeController(ILogger<TeamController> logger, IGrindstoneService grindstoneService, IEmployeeService employeeService, IGrindstoneEmployeeService grindstoneEmployeeService)
        {
            _logger = logger;
            _grindstoneService = grindstoneService;
            _employeeService = employeeService;
            _grindstoneEmployeeService = grindstoneEmployeeService;
        }


        [HttpGet("/api/assignments/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _grindstoneEmployeeService.GetAllAsync();
            if (!assignments.Any())
            {
                var error = new ApiError("No Assignments were found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }
            Console.WriteLine("TEST");
            Console.WriteLine(assignments);

            var assignmentsDetailed = new List<AssignmentModel>();
            foreach (var assignment in assignments)
            {
                var employee = await _employeeService.GetByIdAsync(assignment.EmployeeId);
                var grindstone = await _grindstoneService.GetByIdAsync(assignment.GrindstoneId);
                assignmentsDetailed.Add(new AssignmentModel()
                {
                    Id = assignment.Id,
                    EmployeeId = employee.Id,    
                    TeamId = employee.TeamId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,

                    GrindstoneId = grindstone.Id,
                    TimeToFinish = grindstone.TimeToFinish,
                    Description = grindstone.Description,
                    Priority = grindstone.Priority,
                    StatusId = grindstone.StatusId

                });
            }

            return Ok(assignmentsDetailed);
        }

        [HttpGet("/api/assignments/GetById/{assignmentId}")]
        public async Task<IActionResult> GetById([FromRoute] int assignmentId)
        {
            var assignment = await _grindstoneEmployeeService.GetByIdAsync(assignmentId);

            if (assignment == null)
            {
                var error = new ApiError("Assignment with such Id was not found.", HttpStatusCode.NotFound);
                return NotFound(error);
            }
            var employee = await _employeeService.GetByIdAsync(assignment.EmployeeId);
            var grindstone = await _grindstoneService.GetByIdAsync(assignment.GrindstoneId);

            var assignmentsDetailed = new AssignmentModel()
            {
                Id = assignment.Id,
                EmployeeId = employee.Id,
                TeamId = employee.TeamId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,

                GrindstoneId = grindstone.Id,
                TimeToFinish = grindstone.TimeToFinish,
                Description = grindstone.Description,
                Priority = grindstone.Priority,
                StatusId = grindstone.StatusId

            };

            return Ok(assignmentsDetailed);
        }

        [HttpPost("/api/assignments/Create")]
        public async Task<IActionResult> Create([FromBody] GrindstoneEmployeeDTO assignmentData)
        {
            var assignment = await _grindstoneEmployeeService.GetByIdAsync(assignmentData.Id);

            if (assignment != null)
            {
                var error = new ApiError("Assignment with such Id already exists.", HttpStatusCode.BadRequest);
                return BadRequest(error);
            }
            assignment = await _grindstoneEmployeeService.CreateAsync(assignmentData);


            return Ok(assignment);
        }

        [HttpPut("/api/assignments/Update")]
        public async Task<IActionResult> Update([FromBody] GrindstoneEmployeeDTO assignmentData)
        {
            var assignment = await _grindstoneEmployeeService.GetByIdAsync(assignmentData.Id);

            if (assignment == null)
            {
                var error = new ApiError("Assignment with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }
            if (!await _grindstoneEmployeeService.UpdateAsync(assignmentData))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok(assignmentData);
        }

        [HttpDelete("/api/assignments/DeleteById/{grindstoneId}")]
        public async Task<IActionResult> Delete([FromRoute] int grindstoneId)
        {
            var assignment = await _grindstoneEmployeeService.GetByIdAsync(grindstoneId);

            if (assignment == null)
            {
                var error = new ApiError("Assignment with such Id does not exist.", HttpStatusCode.NotFound);
                return NotFound(error);
            }

            if (!await _grindstoneEmployeeService.DeleteByIdAsync(grindstoneId))
            {
                var error = new ApiError("Unexpecrt error.", HttpStatusCode.Conflict);
                return Conflict(error);
            }

            return Ok();
        }
    }
}
