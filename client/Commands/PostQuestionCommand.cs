using client.Global;
using client.Models;
using client.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace client.Commands
{
    public class PostQuestionCommand : Command
    {
        public PostQuestionCommand() : base("Post a Question", "P")
        {
        }

        public override async Task<bool> Execute()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(
                                HttpMethod.Post,
                                $"{ClientConfiguration.ApiDomain}/api/questions"
                                );
                request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                Question question = ReaderWriter.GetQuestion();

                string jsonBody = JsonSerializer.Serialize(question, new JsonSerializerOptions
                {
                    WriteIndented = false, // Optional: Set to true for pretty-printing
                    IgnoreNullValues = false // Optional: Set to false to include null values
                });

                request.Content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = httpClient.Send(request);

                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                return true;
            }
        }
    }
}
