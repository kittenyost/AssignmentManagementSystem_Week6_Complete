namespace AssignmentManagement.Core
{
    public class AssignmentService : IAssignmentService
    {
        private readonly List<Assignment> _assignments = new();
        private readonly IAssignmentFormatter _formatter;
        private readonly IAppLogger _logger;

        public AssignmentService(IAssignmentFormatter formatter, IAppLogger logger)
        {
            _formatter = formatter;
            _logger = logger;
        }

        public Assignment? FindByTitle(string title)
        {
            return _assignments.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public bool AddAssignment(Assignment assignment)
        {
            if (_assignments.Any(a => a.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            _assignments.Add(assignment);
            _logger.Log($"Added: {_formatter.Format(assignment)}");
            return true;
        }

        public List<Assignment> ListAll()
        {
            return new List<Assignment>(_assignments);
        }

        public List<Assignment> ListIncomplete()
        {
            return _assignments.Where(a => !a.IsCompleted).ToList();
        }

        public Assignment FindAssignmentByTitle(string title)
        {
            return _assignments.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public bool MarkAssignmentComplete(string title)
        {
            var assignment = FindAssignmentByTitle(title);
            if (assignment == null)
                return false;
            assignment.MarkComplete();
            _logger.Log($"Marked complete: {_formatter.Format(assignment)}");
            return true;
        }

        public bool DeleteAssignment(string title)
        {
            var assignment = FindAssignmentByTitle(title);
            if (assignment == null)
                return false;
            _assignments.Remove(assignment);
            _logger.Log($"Deleted: {_formatter.Format(assignment)}");
            return true;
        }

        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription)
        {
            var assignment = FindAssignmentByTitle(oldTitle);
            if (assignment == null)
                return false;

            try
            {
                assignment.Update(newTitle, newDescription);
                _logger.Log($"Updated: {_formatter.Format(assignment)}");
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
