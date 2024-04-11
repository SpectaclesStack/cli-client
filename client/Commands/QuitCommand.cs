using client.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class QuitCommand : Command
    {
        public QuitCommand() : base("Quit", "Q")
        {
        }

        public override async Task<bool> execute()
        {
            Console.WriteLine($"Bye {ClientConfiguration.user}.");
            return false;
        }
    }
}
