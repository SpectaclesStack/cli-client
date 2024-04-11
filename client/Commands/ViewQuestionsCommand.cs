using client.Global;
using client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace client.Commands
{
    public class ViewQuestionsCommand : Command
    {
        public ViewQuestionsCommand() : base("View Questions", "V")
        {
        }

        public override async Task<bool> execute()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{ClientConfiguration.ApiDomain}/api/questions");
                //request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                HttpResponseMessage response = httpClient.Send(request);

                var questionsList = JsonSerializer.Deserialize<List<Question>>(response.Content.ReadAsStringAsync().Result);

                ClientConfiguration.Questions = questionsList;

                ClientConfiguration.questionsMap = new();
                List<Command> commands = new();
                
                int count = 1;

                foreach (var obj in ClientConfiguration.Questions)
                {
                    ClientConfiguration.questionsMap[count] = obj;
                    commands.Add(new SelectQuestionCommand(obj.Title, count.ToString()));
                    count++;
                }

                commands.AddRange(ClientConfiguration.LogoutQuit);

                ClientConfiguration.currentCommands = commands;

                return true;
            }
        }
    }
}
