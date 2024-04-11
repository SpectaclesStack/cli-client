using client.Global;
using client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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


            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage requestAnswers = new HttpRequestMessage(HttpMethod.Get, $"{ClientConfiguration.ApiDomain}/api/Answers");
                //request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                HttpResponseMessage responseAnswers = httpClient.Send(requestAnswers);
                var answersList = JsonSerializer.Deserialize<List<Answer>>(responseAnswers.Content.ReadAsStringAsync().Result);
                ClientConfiguration.Answers = answersList;
            }

            foreach (var answer in ClientConfiguration.Answers)
            {
                if (answer.QuestionId.Equals(ClientConfiguration.questionsMap[int.Parse(Flag)].QuestionId))
                {
                    Console.WriteLine(answer);
                }
            }

            List<Command> commands = [
                new AnswerQuestionCommand(int.Parse(Flag)), .. ClientConfiguration.LogoutQuit 
            ];

            ClientConfiguration.currentCommands = commands;

            return true;
        }
    }
}
