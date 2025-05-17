using AssignmentManagement.Core;

namespace AssignmentManagement.Api.Models
{
    public class AssignmentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
    }
}