using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace client.Models
{
    public class User
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("dateCreated")]
        public DateTime CreateAt { get; set; }

        public override string ToString()
        {
            return $"User ID: {UserId}, Username: {UserName}";
        }
    }
}
