using client.Commands;
using client.Global;
using client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Outputs
{
    internal static class WelcomeOutput
    {
        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to SpectacleStack");
        }

        public static void PrintUserOptions(List<Command> commands)
        {
            Console.WriteLine($"\nHi {ClientConfiguration.user ?? "\"user\""}. Choose an option");
            Console.WriteLine("------------------------------------------------------------");
            foreach (var command in commands)
            {
                if ((ClientConfiguration.accessToken == null || ClientConfiguration.accessToken.Length == 0) && command.Flag == "X")
                    continue;
                if (ClientConfiguration.accessToken != null && ClientConfiguration.accessToken.Length > 0 && command.Flag == "L")
                    continue;
                Console.WriteLine($"{command.Flag} - {command.Name}");
            }
            Console.WriteLine("------------------------------------------------------------");
        }

        public static string PrintInputPrompt()
        {
            return GetUserInput().ToUpper();
        }

        public static string GetUserInput()
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            return input;
        }

        public static Question GetQuestion()
        {
            Console.WriteLine("What is your topic? ");
            var questionTopic = GetUserInput();

            while (questionTopic == "")
            {
                Console.WriteLine("Topic cannot be empty. What is your topic? ");
                questionTopic = GetUserInput();
            }

            Console.WriteLine("What is your question? ");
            var questionBody = GetUserInput();

            while (questionBody == "")
            {
                Console.WriteLine("Question cannot be empty. What is your question? ");
                questionBody = GetUserInput();
            }

            User user = new User();

            user.UserId = 46;
            user.UserName = ClientConfiguration.user == null ? $"new usr {new Random().Next(10, 100)}" : ClientConfiguration.user;
            user.CreateAt = DateTime.UtcNow;

            Question question = new Question();

            question.Title = questionTopic;
            question.Body = questionBody;
            question.CreateAt = DateTime.UtcNow;
            question.User = user.UserId;

            return question;
        }

        public static Answer GetAnswer()
        {
            Console.WriteLine("Enter your Answer ");
            var answer = GetUserInput();

            while (answer == "")
            {
                Console.WriteLine("Answer cannot be empty.");
                answer = GetUserInput();
            }

            Answer answwer = new()
            {
                AnswerId = 0,
                UserId = ClientConfiguration.UserInfo == null ? 7 : ClientConfiguration.UserInfo.UserId,
                QuestionId = 0,
                AnswerString = answer,
                CreateAt = DateTime.UtcNow
            };

            return answwer;
        }
    }
}
