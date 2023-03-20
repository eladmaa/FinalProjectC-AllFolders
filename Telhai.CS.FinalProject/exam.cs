using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telhai.CS.FinalProject
{
    public class Exam
    {
        public string examName { get; set; }
       
        public string id { get; set; }
      
        public DateTime date { get; set; }
       
        public string TeacherName { get; set; }
       
        public int BeginTime { get; set; }  // minutes in the day when exam begins 10:00 => 600, range 0 - 1439
       
        public float duration { get; set; } // duration in hours
       
        public bool isRandom { get; set; }
        public List<Question> questions { get; set; }

        public Exam(string exName)
        {
            this.examName = exName;
            this.id = Guid.NewGuid().ToString();
        }
        [JsonConstructorAttribute]
        public Exam(string name, string teacher_name, DateTime dateTime, int when, int howLong, bool isRan, List<Question> questions)
        {
            questions = new List<Question>();
            this.examName = name;
            this.id = Guid.NewGuid().ToString();
            this.date = dateTime;
            this.TeacherName = teacher_name;
            this.isRandom = isRan;
            this.BeginTime = when;
            this.duration = howLong;
            foreach (Question question in questions)
            {
                this.questions.Add(question);
            }
            

        }
    }
}
