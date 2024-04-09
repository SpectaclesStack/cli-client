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
    public class PostQuestionCommand : Command
    {
        public PostQuestionCommand() : base("Post a Question", "P")
        {
        }

        public override bool execute()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(
                                HttpMethod.Post,
                                $"{ClientConfiguration.ApiDomain}/api/questions"
                                );
                //request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                User user = new User();

                user.UserName = $"{ClientConfiguration.user} {new Random().Next(10, 100)}";
                user.CreateAt = DateTime.UtcNow;


                Question question = new Question();

                question.Title = "What is Lorem Ipsum?";
                question.Body = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
                question.CreateAt = DateTime.UtcNow;
                question.User = user;

                string jsonBody = JsonSerializer.Serialize(question, new JsonSerializerOptions
                {
                    WriteIndented = false, // Optional: Set to true for pretty-printing
                    IgnoreNullValues = false // Optional: Set to false to include null values
                });

                request.Content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");

                Console.WriteLine(request.Content.ReadAsStringAsync().Result);

                HttpResponseMessage response = httpClient.Send(request);

                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                return true;
            }
        }
    }
}
