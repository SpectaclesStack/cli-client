using client.Global;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;

namespace client.Commands
{
    public class LoginCommand : Command
    {
        public LoginCommand() : base("Login", "L")
        {
            
        }
        public override bool execute()
        {
            Console.WriteLine("Logging in....\n");

            try
            {
                if (Authenticate())
                {
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            HttpRequestMessage request = new HttpRequestMessage(
                                HttpMethod.Post, 
                                $"{ClientConfiguration.ApiDomain}/api/users"
                                );
                            request.Headers.Add("Authorization", ClientConfiguration.accessToken);

                            string jsonBody = $"{{\"userId\":\"0\",\"email\":\"dummyemail@gmail.com\",\"userName\":{ClientConfiguration.user}, \"createAt\":null}}";

                             Console.WriteLine(jsonBody);

                            request.Content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");


                            HttpResponseMessage response = httpClient.Send(request);
                            if (response.StatusCode.Equals(HttpStatusCode.OK))
                            {
                                Console.WriteLine("Successfully logged in!");
                                return true; 
                            }
                            else
                            {
                                Console.WriteLine(response);
                                throw new Exception();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occured while logging in....");
                        ClientConfiguration.accessToken = "";
                        ClientConfiguration.user = "user";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while authenticaing...");
                ClientConfiguration.accessToken = "";
                ClientConfiguration.user = "user";
                return true;
            }

            Console.WriteLine("Login sucessful...");
            return true;
        }
    }
}

