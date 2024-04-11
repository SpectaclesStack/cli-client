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

        public override async Task<bool> Execute()
        {
            try
            {
                using (HttpClient httpClient = new())
                {
                    HttpRequestMessage request = new(HttpMethod.Get, $"{ClientConfiguration.ApiDomain}/api/questions");
                    request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                    HttpResponseMessage response = httpClient.Send(request);

                    List<Question>? questionsList = JsonSerializer.Deserialize<List<Question>>(response.Content.ReadAsStringAsync().Result);

                    ClientConfiguration.Questions = questionsList;

                    ClientConfiguration.questionsMap = [];
                    List<Command> commands = [];

                    int count = 1;

                    ClientConfiguration.Questions?.ForEach(obj =>
                    {
                        ClientConfiguration.questionsMap[count] = obj;
                        commands.Add(new SelectQuestionCommand(obj.Title, count.ToString()));
                        count++;
                    });

                    commands.AddRange(ClientConfiguration.LogoutQuit);

                    ClientConfiguration.currentCommands = commands;
                }

            }
            catch
            {
                Console.WriteLine("Error occured..Could Not retrieve questions.");
                return true;
            }


            return true;
        }
    }
}
