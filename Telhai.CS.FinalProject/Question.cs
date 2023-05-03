using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Telhai.CS.FinalProject
{
    public class Question
    {
        [JsonPropertyName("content")]

        public string content { get; set; }
        [JsonPropertyName("correct")]

        public int correct { get; set; }
        [JsonPropertyName("answers")]

        public List<string> answers;
        [JsonPropertyName("_id")]

        public Guid _id;
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
   //     [JsonPropertyName("QImage")]
    //    public byte[] QImage { get; set; }

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
