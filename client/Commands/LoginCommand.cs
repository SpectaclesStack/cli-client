using client.Global;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
            Console.WriteLine("\nLogging in....\n");

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


                            HttpResponseMessage response = httpClient.Send(request);
                            if (response.StatusCode.Equals(HttpStatusCode.OK))
                            {
                                Console.WriteLine("Successfully logged in!\n");
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
                        Console.WriteLine("An error occured while logging in....\n");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while authenticaing...\n");
                return true;
            }

            Console.WriteLine("\nLogin sucessful...\n");
            return true;
        }
    }
}

