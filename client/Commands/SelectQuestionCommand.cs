using client.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class SelectQuestionCommand : Command
    {
        public SelectQuestionCommand(string name, string flag) : base(name, flag)
        {
        }

        public override bool execute()
        {
            Console.WriteLine(ClientConfiguration.questionsMap[int.Parse(Flag)]);


            List<Command> commands = [
                new AnswerQuestionCommand(int.Parse(Flag)), .. ClientConfiguration.LogoutQuit 
            ];

            ClientConfiguration.currentCommands = commands;

            return true;
        }
    }
}
