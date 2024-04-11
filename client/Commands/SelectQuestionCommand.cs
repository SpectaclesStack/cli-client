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

        public override async Task<bool> Execute()
        {
            Console.WriteLine(ClientConfiguration.questionsMap?[int.Parse(Flag)]);

            try
            {
                using (HttpClient httpClient = new())
                {
                    HttpRequestMessage requestAnswers = new(HttpMethod.Get, $"{ClientConfiguration.ApiDomain}/api/Answers");
                    requestAnswers.Headers.Add("Authorization", ClientConfiguration.accessToken);

                    HttpResponseMessage responseAnswers = httpClient.Send(requestAnswers);
                    var answersList = JsonSerializer.Deserialize<List<Answer>>(responseAnswers.Content.ReadAsStringAsync().Result);
                    ClientConfiguration.Answers = answersList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to fetch answers to selected question.");
                return true;
            }
            

            foreach (var answer in ClientConfiguration.Answers)
            {
                if (answer.QuestionId.Equals(ClientConfiguration.questionsMap?[int.Parse(Flag)].QuestionId))
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
