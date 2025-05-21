using Microsoft.AspNetCore.Mvc;
using AssignmentManagement.Core;
using AssignmentManagement.Api.Models;

namespace AssignmentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AssignmentDto>> GetAll()
        {
            var assignments = _assignmentService.ListAll();

            var dtos = assignments.Select(a => new AssignmentDto
            {
                Title = a.Title,
                Description = a.Description,
                Priority = a.Priority
            });

            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AssignmentDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Assignment data is required.");
            }

            var assignment = new Assignment(dto.Title, dto.Description, dto.Priority);
            var success = _assignmentService.AddAssignment(assignment);

            if (!success)
                return Conflict("An assignment with this title already exists.");

            return CreatedAtAction(nameof(GetAll), new { title = dto.Title }, dto);
        }

        [HttpDelete("{title}")]
        public IActionResult Delete(string title)
        {
            var result = _assignmentService.DeleteAssignment(title);
            if (!result)
            {
                return NotFound("Assignment not found.");
            }

            return NoContent();
        }
    }
}