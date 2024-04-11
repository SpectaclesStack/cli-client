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
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{ClientConfiguration.ApiDomain}/api/answers");
                //request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                Answer answer = WelcomeOutput.GetAnswer();
                answer.QuestionId = ClientConfiguration.questionsMap[_questionId].QuestionId;

                string jsonBody = JsonSerializer.Serialize(answer, new JsonSerializerOptions
                {
                    WriteIndented = false,
                    IgnoreNullValues = false
                });

                request.Content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = httpClient.Send(request);

                Thread.Sleep(3000);

                HttpRequestMessage requestAnswers = new HttpRequestMessage(HttpMethod.Get, $"{ClientConfiguration.ApiDomain}/api/Answers");
                //requestAnswers.Headers.Add("Authorization", ClientConfiguration.accessToken);

                HttpResponseMessage responseAnswers = httpClient.Send(requestAnswers);
                var answersList = JsonSerializer.Deserialize<List<Answer>>(responseAnswers.Content.ReadAsStringAsync().Result);
                ClientConfiguration.Answers = answersList;

                Console.WriteLine(ClientConfiguration.questionsMap[_questionId]);

                foreach (var answers in ClientConfiguration.Answers)
                {
                    if (answers.QuestionId == answer.QuestionId)
                    {
                        Console.WriteLine(answers);
                    }
                }

                return true;
            }
        }
    }
}
