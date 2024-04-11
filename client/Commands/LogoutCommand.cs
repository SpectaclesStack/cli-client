using client.Global;
using client.Models;

namespace client.Commands
{
    public class LogoutCommand : Command
    {
        public LogoutCommand() : base("Logout", "X")
        {
        }

        public override Task<bool> Execute()
        {
            ClientConfiguration.accessToken = "";
            ClientConfiguration.User = new User() { UserName = "user" };
            Console.WriteLine("Successfully Logged Out!");
            ClientConfiguration.currentCommands = ClientConfiguration.defaultcommands;
            return Task.FromResult(true);
        }
    }
}
