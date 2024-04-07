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

        public override bool execute()
        {
            Console.WriteLine("Bye");
            return false;
        }
    }
}
