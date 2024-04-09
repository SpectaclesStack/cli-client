using client.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace client.Commands
{
    public class ViewQuestionsCommand : Command
    {
        public ViewQuestionsCommand() : base("View all questions", "V")
        {
        }

        public override bool execute()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(
                                HttpMethod.Get,
                                $"{ClientConfiguration.ApiDomain}/api/questions"
                                );
                //request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                HttpResponseMessage response = httpClient.Send(request);

                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                return true;
            }
        }
    }
}
