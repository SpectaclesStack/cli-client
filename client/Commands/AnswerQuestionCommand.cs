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
    public class AnswerQuestionCommand : Command
    {
        private int _questionId;
        public AnswerQuestionCommand(int questionId) : base("Answer Question", "A")
        {
            _questionId = questionId;
        }

        public override bool execute()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(
                                HttpMethod.Post,
                                $"{ClientConfiguration.ApiDomain}/api/answers"
                                );
                //request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                Answer answer = WelcomeOutput.GetAnswer();
                answer.QuestionId = _questionId;

                string jsonBody = JsonSerializer.Serialize(answer, new JsonSerializerOptions
                {
                    WriteIndented = false, // Optional: Set to true for pretty-printing
                    IgnoreNullValues = false // Optional: Set to false to include null values
                });

                Console.WriteLine(jsonBody);

                request.Content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");

                //Console.WriteLine(request.Content.ReadAsStringAsync().Result);

                HttpResponseMessage response = httpClient.Send(request);

                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                return true;
            }
        }
    }
}
