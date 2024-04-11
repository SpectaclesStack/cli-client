using client.Commands;
using client.Global;
using client.Outputs;

namespace client
{
    internal class Program
    {
        private CommandHandler _handler = new();
        private async Task Start()
        {
            ReaderWriter.PrintWelcomeMessage();

            bool continueExecution = true;

            while (continueExecution)
            {
                _handler.Commands = ClientConfiguration.currentCommands;

                ReaderWriter.PrintUserOptions(_handler.Commands);                

                var userSelection = ReaderWriter.PrintInputPrompt();

                try
                {
                    continueExecution = await _handler.getCommand(userSelection).Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred...\nTry Again.\n");
                }
            }
        }

        static async Task Main(string[] args)
        {
           await new Program().Start();
        }
    }
}
