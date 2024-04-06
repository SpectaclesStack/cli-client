using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class CommandHandler
    {
        private List<Command> CommandList;

        public List<Command> Commands
        {
            get => CommandList;
            set => CommandList = value;
        }

        public Command getCommand(string flag)
        {
            return Commands.FirstOrDefault(obj => obj.Flag.Equals(flag)) ?? new InvalidCommand(flag);
        }

    }
}
