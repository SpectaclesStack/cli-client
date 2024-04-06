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

        public override bool execute()
        {
            ClientConfiguration.accessToken = "";
            Console.WriteLine("\nSuccessfully Logged Out!");
            return true;
        }
    }
}
