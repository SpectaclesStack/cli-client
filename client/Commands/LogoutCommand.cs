using client.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class LogoutCommand : Command
    {
        public LogoutCommand() : base("Logout", "X")
        {
        }

        public override async Task<bool> execute()
        {
            ClientConfiguration.accessToken = "";
            ClientConfiguration.user = "user";
            Console.WriteLine("Successfully Logged Out!");
            return true;
        }
    }
}
