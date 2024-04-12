using client.Commands;
using client.Models;

namespace client.Global
{
    public static class ClientConfiguration
    {
        public static string ClientId = Environment.GetEnvironmentVariable("CLIENT_ID") ?? "b03ee56bb0f38ebef184";
        public const string RedirectUri = "http://localhost";
        public const string AuthorizationEndpoint = "https://github.com/login/device/code";
        public const string TokenEndpoint = "https://github.com/login/oauth/access_token";
        public const string Scope = "read:user";
        public const string GrantType = "urn:ietf:params:oauth:grant-type:device_code";
        public const string ApiDomain = "http://3.250.62.135:5033";
        //public const string ApiDomain = "http://localhost:5033";

        public static string? accessToken { get; set; }

        public static User? User { get; set; }

        public static List<Command> defaultcommands = [
            new LoginCommand(),
            new LogoutCommand(),
            new QuitCommand(),
        ];

        public static List<Command> HomeScreenCommands = [
            new LoginCommand(),
            new LogoutCommand(),
            new QuitCommand(),
            new ViewQuestionsCommand(),
            new PostQuestionCommand(),
        ];

        public static List<Command> LogoutQuit = [
            new LoginCommand(),
            new LogoutCommand(),
            new HomeCommand(),
            new QuitCommand(),
        ];

        public static List<Command> currentCommands = defaultcommands;

        public static List<Question>? Questions = [];

        public static List<Answer>? Answers = [];

        public static Dictionary<int, Question>? questionsMap = new();
    }
}
