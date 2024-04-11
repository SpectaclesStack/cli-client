using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace client.Models
{
    public class Answer
    {
        [JsonPropertyName("questionid")]
        public int QuestionId { get; set; }

        [JsonPropertyName("answerId")]
        public int AnswerId { get; set; }

        [JsonPropertyName("userid")]
        public int UserId { get; set; }

        [JsonPropertyName("answer")]
        public string AnswerString { get; set; }

        [JsonPropertyName("createAt")]
        public DateTime CreateAt { get; set; }

        public override string ToString()
        {
            return $"Answer: {AnswerString}\nCreated At: {CreateAt}\n";
        }
    }
}
