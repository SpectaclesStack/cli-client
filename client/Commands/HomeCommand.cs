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

        public override async Task<bool> Execute()
        {
            ClientConfiguration.currentCommands = ClientConfiguration.HomeScreenCommands;
            return true;
        }
    }
}
