using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZstdSharp.Unsafe;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Telhai.CS.FinalProject
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        Exam exam;
        public Form1 timer;
        public Test(Exam ex)
        {
            InitializeComponent();
            this.exam = new Exam();
            this.exam = ex;
            this.txtExamName.Text = exam.examName;
            this.txtID.Text = exam.examId;
            this.txtTeacher.Text = exam.TeacherName;
            this.time_begining.Text = exam.BeginTime.ToString();
            this.QuestionsLB.Items.Clear();
            foreach(Question q in exam.questions.ToArray())
            {
                this.QuestionsLB.Items.Add(q);
            }
            QuestionsLB.Items.Refresh();
            
         //   this.exame_Datepicker = exam.date;
            timer = new Form1(ex.duration*60*60);
            timer.TimerFinished += Form1_TimerFinished;
            timer.Show();
        }


        private void Form1_TimerFinished(object sender, EventArgs e)
        {
            submit_Exam_Click(sender, (RoutedEventArgs)e);
        }

        private async void submit_Exam_Click(object sender, RoutedEventArgs e)
        {
            Exam exam = new Exam();

            if (QuestionsLB.Items.Count != 0)
            {
                foreach (var question in QuestionsLB.Items)
                {
                    Question q = question as Question;
                    exam.questions.Add(q);
                }
            }
            exam.TeacherName = this.txtTeacher.Text;
            exam.examName = this.txtExamName.Text;

            exam.BeginTime = DateTime.Parse(time_begining.Text);
            exam.examId = this.txtID.Text;

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7109/");
            StringContent reqBody = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Exams/answers/" + 0, reqBody);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.Windows.MessageBox.Show($" Exam - {exam.examName} has been submitted for evaluation successfuly");
                timer.Close();
                Close();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                System.Windows.MessageBox.Show($" Exam - {exam.examName} can only be submitted once");
                timer.Close();

                Close();
            }
        }

        private void QuestionsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.QuestionsLB.SelectedItem != null)
            {
                this.AnswersListBox.Items.Clear();
                if (this.QuestionsLB.SelectedItem is Question question)
                {
                    this.textQuestion.Text = question.content.ToString();
                    foreach (string answer in question.answers.ToArray())
                    {
                        this.AnswersListBox.Items.Add(answer);
                    }

                    int temp = question.correct;
                    this.Correct_answer.Items.Clear();
                    foreach (var answer in question.answers)
                    {
                        Correct_answer.Items.Add(answer);
                    }
                    this.Correct_answer.SelectedIndex = temp;
                }

                
            }
        }

        private void txtExame_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Correct_answer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.QuestionsLB.SelectedItem is Question question)
            {
                question.correct = this.Correct_answer.SelectedIndex;
            }
        }
    }
}
