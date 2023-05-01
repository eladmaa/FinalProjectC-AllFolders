using System.Text.Json.Serialization;

namespace Telhai.CS.APIServer.Models
{
    public class Question
    {
        [JsonPropertyName("QImage")]
        public byte[] QImage { get; set; }

        [JsonPropertyName("content")]

        public string content { get; set; }
        [JsonPropertyName("correct")]

        public int correct { get; set; }
        [JsonPropertyName("answers")]

        public List<string> answers;
        [JsonPropertyName("_id")]

        private Guid _id;
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public Question()
        {
            answers = new List<string>();
        }
        public void add(string answer)
        {
            answers.Add(answer);
        }
        public int answersCount()
        {
            return answers.Count;
        }
    }


}
