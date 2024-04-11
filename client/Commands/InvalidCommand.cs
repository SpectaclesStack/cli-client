using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class InvalidCommand : Command
    {
        public InvalidCommand(string flag) : base($"{flag} is and Invalid Selection", "" )
        {
            
        }
        public override Task<bool> Execute()
        {
            Console.WriteLine(Name);
            return Task.FromResult(true);
        }
    }
}
