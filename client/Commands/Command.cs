using client.Global;
using client.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace client.Commands
{
    public abstract class Command
    {
        private string commandName;
        private string commandFlag;

        public Command(string name, string flag)
        {
            commandName = name;
            commandFlag = flag;
        }

        public string Name
        {
            get => commandName;
            set => commandName = value;
        }

        public string Flag
        {
            get => commandFlag;
            set => commandFlag = value;
        }

        public bool Authenticate()
        {
            string logFilePath = "error.log";

            // Configure trace listeners to write logs to a file
            TextWriterTraceListener logListener = new(File.Create(logFilePath));
            Trace.Listeners.Add(logListener);

            try
            {
                bool success = false;

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpRequestMessage deviceCodeRequest = new HttpRequestMessage(HttpMethod.Post, $"{ClientConfiguration.AuthorizationEndpoint}?client_id={ClientConfiguration.ClientId}&scope={ClientConfiguration.Scope}");

                    HttpResponseMessage deviceCodeResponse = httpClient.Send(deviceCodeRequest);
                    deviceCodeResponse.EnsureSuccessStatusCode();

                    string deviceCodeResponseBody = deviceCodeResponse.Content.ReadAsStringAsync().Result;

                    NameValueCollection queryParameters = HttpUtility.ParseQueryString(deviceCodeResponseBody);

                    string userCode = queryParameters["user_code"];
                    string deviceCode = queryParameters["device_code"];
                    string verificationUri = queryParameters["verification_uri"];

                    Console.WriteLine($"Please go to {verificationUri} and enter the code: {userCode}\n");


                    while (!success)
                    {
                        Thread.Sleep(5000);

                        HttpRequestMessage accessTokenRequest = new HttpRequestMessage(HttpMethod.Post,
                            $"{ClientConfiguration.TokenEndpoint}?client_id={ClientConfiguration.ClientId}&device_code={deviceCode}&grant_type={ClientConfiguration.GrantType}");

                        HttpResponseMessage accessTokenResponse = httpClient.Send(accessTokenRequest);
                        accessTokenResponse.EnsureSuccessStatusCode();

                        string accessTokenResponseBody = accessTokenResponse.Content.ReadAsStringAsync().Result;

                        if (!accessTokenResponseBody.StartsWith("error"))
                        {
                            queryParameters = HttpUtility.ParseQueryString(accessTokenResponseBody);

                            ClientConfiguration.accessToken = $"Bearer {queryParameters["access_token"]}";

                            HttpRequestMessage userInfoRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/user");

                            userInfoRequest.Headers.Add("Authorization", ClientConfiguration.accessToken);
                            userInfoRequest.Headers.Add("User-Agent", "SpectaclesStack");

                            HttpResponseMessage response = httpClient.Send(userInfoRequest);

                            using (JsonDocument doc = JsonDocument.Parse(response.Content.ReadAsStringAsync().Result))
                            {
                                JsonElement root = doc.RootElement;

                                User user = new User();
                                user.UserName = root.GetProperty("login").GetString();

                                ClientConfiguration.User = user;
                            }

                            success = true;
                        }

                        if (accessTokenResponseBody.Contains("access_denied"))
                        {
                            throw new Exception("Auth failed");
                        }
                    }
                }

                return success;
            }
            catch (Exception ex) 
            {
                Trace.TraceError($"An error occurred: {ex.Message}");
                return false;
            }
            finally
            {
                // Close the trace listener
                logListener.Close();
            }
            

        }

        public abstract Task<bool> Execute();

    }
}
