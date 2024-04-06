using client.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Outputs
{
    internal static class WelcomeOutput
    {
        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to SpectacleStack\n");
        }

        public static void PrintUserOptions()
        {
            Console.WriteLine("\nChoose an option");
            foreach (var command in ClientConfiguration.ValidCommands)
            {
                if (ClientConfiguration.accessToken == null && command.Flag == "X")
                    continue;
                if (ClientConfiguration.accessToken != null && ClientConfiguration.accessToken.Length > 0 && command.Flag == "L")
                    continue;
                Console.WriteLine($"{command.Flag} - {command.Name}");
            }
            Console.WriteLine();
        }

        public static string PrintInputPrompt()
        {
            Console.Write("Selection: ");
            var input = Console.ReadLine();
            Console.WriteLine();
            return input.ToUpper();
        }
    }
}
