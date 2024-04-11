using client.Global;
using client.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
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
        public override async Task<bool> Execute()
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

                            User user = new();

                            user.UserName = ClientConfiguration.User.UserName;
                            user.CreateAt = DateTime.UtcNow;

                            string jsonBody = JsonSerializer.Serialize(user, new JsonSerializerOptions
                            {
                                WriteIndented = false,
                                IgnoreNullValues = false
                            });

                            request.Content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");


                            HttpResponseMessage response = httpClient.Send(request);
                            if (response.StatusCode.Equals(HttpStatusCode.OK))
                            {
                                var responseUser = JsonSerializer.Deserialize<User>(response.Content.ReadAsStringAsync().Result);
                                ClientConfiguration.User = responseUser;

                                Console.WriteLine("Successfully logged in!");
                                ClientConfiguration.currentCommands = ClientConfiguration.HomeScreenCommands;
                                return true; 
                            }
                            else
                            {
                                Console.WriteLine(response);
                                throw new Exception("Failed to login.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occured while authenticaing.");
                    }
                }
                else
                {
                    throw new Exception("An error occured while authenticaing...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ClientConfiguration.accessToken = "";
                ClientConfiguration.User = new();
                return true;
            }
        }
    }
}

