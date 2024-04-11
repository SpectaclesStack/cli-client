using client.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class HomeCommand : Command
    {
        public HomeCommand() : base("Home", "H")
        {
        }

        public override bool execute()
        {
            ClientConfiguration.currentCommands = ClientConfiguration.HomeScreenCommands;
            return true;
        }
    }
}
