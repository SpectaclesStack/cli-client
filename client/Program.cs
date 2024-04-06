using client.Commands;
using client.Global;
using client.Outputs;

namespace client
{
    internal class Program
    {
        private CommandHandler _handler = new();
        private void start()
        {
            _handler.Commands = ClientConfiguration.ValidCommands;

            WelcomeOutput.PrintWelcomeMessage();

            bool continueExecution = true;

            while (continueExecution)
            {
                WelcomeOutput.PrintUserOptions();

                var userSelection = WelcomeOutput.PrintInputPrompt();

                try
                {
                    continueExecution = _handler.getCommand(userSelection).execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred...\nTry Again.\n");
                }
            }
        }

        static void Main(string[] args)
        {
            new Program().start();
        }
    }
}
