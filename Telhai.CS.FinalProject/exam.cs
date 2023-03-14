using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telhai.CS.FinalProject
{
    public class Exam
    {
        [JsonPropertyName("examName")]
        public string examName { get; set; }
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("date")]
        public DateTime date { get; set; }
        [JsonPropertyName("TeacherName")]
        public string TeacherName { get; set; }
        [JsonPropertyName("BeginTime")]
        public DateTime BeginTime { get; set; }
        [JsonPropertyName("duration")]
        public float duration { get; set; }
        [JsonPropertyName("israndom")]
        public bool isRandom { get; set; }
        public List<Question> questions { get; set; }

        public Exam(string exName)
        {
            this.examName = exName;
            this.id = Guid.NewGuid().ToString();
        }
    }
}
