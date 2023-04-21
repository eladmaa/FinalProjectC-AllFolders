using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telhai.CS.APIServer.Models;

namespace Telhai.CS.FinalProject
{
    public interface IExamRepository
    {
        void addExam(Exam exam);
        void updateExam(Exam exam);
        void removeExam(Exam exam);
        Exam getExam(String examName);
        Exam[] Exams { get; }
    }
}
