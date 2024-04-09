﻿using client.Commands;

namespace client.Global
{
    public static class ClientConfiguration
    {
        public static string ClientId = Environment.GetEnvironmentVariable("CLIENT_ID") ?? "";
        public const string RedirectUri = "http://localhost";
        public const string AuthorizationEndpoint = "https://github.com/login/device/code";
        public const string TokenEndpoint = "https://github.com/login/oauth/access_token";
        public const string Scope = "read:user";
        public const string GrantType = "urn:ietf:params:oauth:grant-type:device_code";
        public const string ApiDomain = "http://3.250.62.135:5033";

        public static string accessToken { get; set; }
        public static string user { get; set; }

        public static List<Command> ValidCommands = [
            new LoginCommand(),
            new LogoutCommand(),
            new QuitCommand(),
            new ViewQuestionsCommand(),
            new PostQuestionCommand(),
        ];
    }
}
