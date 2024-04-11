using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class BackCommand : Command
    {
        public BackCommand() : base("Back", "B")
        {
        }

        public override bool execute()
        {
            throw new NotImplementedException();
        }
    }
}
