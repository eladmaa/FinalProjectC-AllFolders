using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telhai.CS.APIServer.Models;

namespace Telhai.CS.FinalProject
{
    public class ExamRepository : IExamRepository
    {
        private List<Exam> _examList;
        static private ExamRepository _instance = null;

        public Exam[] Exams => throw new NotImplementedException();

        private ExamRepository() 
        {
            this._examList= new List<Exam>();
        }
        /*
        public static ExamRepository Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new ExamRepository();
                    return _instance;
            }
          
        }
        */
        public void addExam(Exam exam)
        {
            this._examList.Add(exam);
        }
        
        public Exam getExam(string examName)
        {
            foreach (var exam in _examList)
            {
                if (String.Compare(exam.examName,examName) == 0)
                    return exam;
            }
            return null;
        }

        public void removeExam(Exam exam)
        {
            _examList.Remove(exam);
        }

        public void updateExam(Exam exam)
        {
            int indexFound = this._examList.FindIndex(s => s.Equals(exam));
            if(indexFound >=0) 
            {
                this._examList[indexFound] = exam;
            }
        }

        public List<Exam> getExamList() { return this._examList; }
    }
}
