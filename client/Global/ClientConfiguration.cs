using client.Commands;

namespace client.Global
{
    public static class ClientConfiguration
    {
        public const string ClientId = "";
        //public const string ClientSecret = "your_client_secret";
        public const string RedirectUri = "http://localhost";
        public const string AuthorizationEndpoint = "https://github.com/login/device/code";
        public const string TokenEndpoint = "https://github.com/login/oauth/access_token";
        public const string Scope = "read:user";
        public const string GrantType = "urn:ietf:params:oauth:grant-type:device_code";
        public const string ApiDomain = "https://localhost:5001";

        public static string accessToken { get; set; }
        public static string user { get; set; }

        public static List<Command> ValidCommands = [
            new LoginCommand(),
            new LogoutCommand(),
            new QuitCommand()
        ];
    }
}
