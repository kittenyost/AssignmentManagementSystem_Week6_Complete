using AssignmentManagement.Core;
namespace AssignmentManagement.UI
{
    public class ConsoleAppLogger : IAppLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}
