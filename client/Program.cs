using client.Commands;
using client.Global;
using client.Outputs;

namespace client
{
    internal class Program
    {
        private CommandHandler _handler = new();
        private async void start()
        {
            WelcomeOutput.PrintWelcomeMessage();

            bool continueExecution = true;

            while (continueExecution)
            {
                _handler.Commands = ClientConfiguration.currentCommands;

                WelcomeOutput.PrintUserOptions(_handler.Commands);                

                var userSelection = WelcomeOutput.PrintInputPrompt();

                try
                {
                    continueExecution = await _handler.getCommand(userSelection).execute();
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
