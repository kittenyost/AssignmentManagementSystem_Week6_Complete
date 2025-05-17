using AssignmentManagement.Core;
namespace AssignmentManagement.ConsoleUI
{
    public class ConsoleUI
    {
        private readonly IAssignmentService _service;
        private readonly IAppLogger _logger;

        public ConsoleUI(IAssignmentService service, IAppLogger logger)
        {
            _service = service;
            _logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                System.Console.WriteLine("\n1. Add Assignment");
                System.Console.WriteLine("2. List Assignments");
                System.Console.WriteLine("3. Exit");
                System.Console.Write("Choice: ");
                var choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAssignment();
                        break;
                    case "2":
                        ListAssignments();
                        break;
                    case "3":
                        return;
                    default:
                        System.Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        public void AddAssignment()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.Write("Priority (Low, Medium, High): ");
            var priorityInput = Console.ReadLine();

            Priority priority;
            if (!Enum.TryParse(priorityInput, true, out priority))
            {
                Console.WriteLine("Invalid priority, defaulting to Medium.");
                priority = Priority.Medium;
            }

            try
            {
                var assignment = new Assignment(title, description, priority);
                _service.AddAssignment(assignment);
                _logger.Log($"Added: [{assignment.Id}] {assignment.Title} - {(assignment.IsCompleted ? "Completed" : "Incomplete")}");
                Console.WriteLine("Assignment added.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ListAssignments()
        {
            var assignments = _service.ListAll();
            foreach (var a in assignments)
            {
                Console.WriteLine($"[{a.Id}] {a.Title} - {a.Description} - Priority: {a.Priority} - {(a.IsCompleted ? "✓" : "✗")}");
            }
        }
    }
}