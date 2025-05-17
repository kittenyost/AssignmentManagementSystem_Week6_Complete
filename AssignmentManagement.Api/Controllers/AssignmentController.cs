using AssignmentManagement.Api.Models;
using AssignmentManagement.Core;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _service;

        public AssignmentController(IAssignmentService service)
        {
            _service = service;
        }

        // POST /api/assignment
        [HttpPost]
        public IActionResult CreateAssignment([FromBody] AssignmentDto dto)
        {
            if (dto == null)
                return BadRequest("Assignment data is required.");

            var assignment = new Assignment(dto.Title, dto.Description, dto.Priority);
            var result = _service.AddAssignment(assignment);

            return result
                ? CreatedAtAction(nameof(GetAll), new { title = assignment.Title }, assignment)
                : Conflict("Assignment already exists.");
        }

        // GET /api/assignment
        [HttpGet]
        public IActionResult GetAll()
        {
            var assignments = _service.ListAll();
            return Ok(assignments);
        }

        // DELETE /api/assignment/{title}
        [HttpDelete("{title}")]
        public IActionResult Delete(string title)
        {
            var result = _service.DeleteAssignment(title);
            return result ? NoContent() : NotFound($"Assignment with title '{title}' not found.");
        }
    }
}