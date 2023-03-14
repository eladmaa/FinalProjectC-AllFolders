using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Telhai.CS.APIServer.Models
{
    public class Exam
    {
        public string examName { get; set; }
        [Key]
        public string id { get; set; }
        
        public DateTime date { get; set; }
        public string TeacherName { get; set; }
        public DateTime BeginTime { get; set; }
        public float duration { get; set; }
        public bool isRandom { get; set; }
        public List<Question> questions { get; set; }
        
        public Exam(string exName, string id)
        {
            this.examName = exName;
            this.id = id;
          //  this.id = Guid.NewGuid().ToString();
        }
        
        public Exam() 
        {
            examName = "";
            this.id = Guid.NewGuid().ToString();
        }
    }

    public class Question
    {
        public string content { get; set; }
        public int correct { get; set; }
        List<string> answers;
        private Guid _id;
        [Key]
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
    }


}
