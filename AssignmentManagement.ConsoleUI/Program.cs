using AssignmentManagement.Core;
using AssignmentManagement.UI;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace AssignmentManagement.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setup Dependency Injection
            var services = new ServiceCollection();

            // Register formatter, logger, service, and UI
            services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
            services.AddSingleton<IAppLogger, ConsoleAppLogger>();
            services.AddSingleton<IAssignmentService, AssignmentService>();
            services.AddSingleton<ConsoleUI>();

            // Build the service provider and start the app
            var provider = services.BuildServiceProvider();
            var ui = provider.GetRequiredService<ConsoleUI>();
            ui.Run();
        }

        //public static void Main(string[] args) // 🔥 Static Main method — this is required
        //{
        //    var services = new ServiceCollection();

        //    services.AddSingleton<AssignmentService>();
        //    services.AddSingleton<ConsoleUI>();

        //    var serviceProvider = services.BuildServiceProvider();
        //    var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();

        //    consoleUI.Run();
        //}
    }
}
