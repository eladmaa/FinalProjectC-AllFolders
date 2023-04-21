using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Telhai.CS.APIServer.Models
{
    public class Exam
    {

        // ID, name, content
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

        public Exam(string examName, string id, DateTime date, string teacherName, DateTime beginTime, float duration, bool isRandom, List<Question> questions) : this(examName, id)
        {
            this.date = date;
            TeacherName = teacherName;
            BeginTime = beginTime;
            this.duration = duration;
            this.isRandom = isRandom;
            this.questions = questions;
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
