using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace client.Models
{
    public class Question
    {
        [JsonPropertyName("questionId")]
        public int QuestionId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("createAt")]
        public DateTime CreateAt { get; set; }

        [JsonPropertyName("userid")]
        public int User { get; set; }

        public override string ToString()
        {
            return $"Title: {Title}\nQuestion: {Body}\nCreated2: {CreateAt.ToLocalTime()}\n";
        }
    }
}
