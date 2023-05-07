using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telhai.CS.APIServer.Models;

namespace Telhai.CS.FinalProject
{
    public class ByteArrayConverter : JsonConverter<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            short[] sByteArray = JsonSerializer.Deserialize<short[]>(ref reader);
            byte[] value = new byte[sByteArray.Length];
            for (int i = 0; i < sByteArray.Length; i++)
            {
                value[i] = (byte)sByteArray[i];
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var val in value)
            {
                writer.WriteNumberValue(val);
            }

            writer.WriteEndArray();
        }
    }
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private const float MAX_TEST_DURATION = 3f;
        private const float MIN_TEST_DURATION = 1f;
        static int id = 0;
        static int Qid = 0;

      //  bool isNew = false;
        
        //private List<Exam> exams = new List<Exam>();
        //private ObservableCollection<Question> questionsList = new ObservableCollection<Question>();
        //private ObservableCollection<String> answersList = new ObservableCollection<String>();
        public TeacherWindow()
        {
            InitializeComponent();
          //  this.Loaded += Window_Loaded_1;
            examsList.ItemsSource = HttpExamRepository.Instance.examList;
//            this.QuestionsLB.ItemsSource = questionsList;
            exame_Datepicker.SelectedDate = DateTime.Now;
            time_begining.Text = DateTime.Now.ToString("HH:mm");
            for (float i = MIN_TEST_DURATION; i <= MAX_TEST_DURATION; i += 0.5f)
            {
                time_duration.Items.Add(i.ToString());
            }
            time_duration.Items.Insert(0, "Choose exam duration");
            time_duration.SelectedIndex = 0;
        }
        //****************************************************************************************************

        private async void btn_AddExam_Click(object sender, RoutedEventArgs e)
        {
            id++;
            string idCounter = id.ToString();
            exame_Datepicker.SelectedDate = DateTime.Now;
            Exam exam = new Exam() {examName = "Name_" + idCounter };
            await HttpExamRepository.Instance.AddExamAsync(exam);
            //Reload
            //  List<Exam> list = await 

            //    isNew = true;
            examsList.ItemsSource = HttpExamRepository.Instance.examList;
            examsList.Items.Refresh();
        }
        //****************************************************************************************************

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7109/");
            HttpResponseMessage response = await httpClient.GetAsync("api/Exams");
            if (response != null)
            {
                string? examsString = await response.Content.ReadAsStringAsync();
                HttpExamRepository.Instance.examList = JsonSerializer.Deserialize<List<Exam>>(examsString);
                examsList.ItemsSource = HttpExamRepository.Instance.examList;

            }
            

        }

        //****************************************************************************************************

        private void AddAnswer_btn_Click(object sender, RoutedEventArgs e)
        {
           
                if(QuestionsLB.SelectedItem is Question q)
                {
                    string newAnswer = Interaction.InputBox("Enter new answer:", "New Answer", "", 0, 0);
                    if (newAnswer != "")
                    {
                       
                        q.answers.Add(newAnswer);
                        AnswersListBox.Items.Add(newAnswer);
                        AnswersListBox.SelectedItem = newAnswer;

                        this.Correct_answer.Items.Add(newAnswer);

                }

                
                

            }
            

        }
        //****************************************************************************************************


        private void btnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
           "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
           "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
              /*  try
                {
                    if(this.QuestionsLB.SelectedItem is Question q) 
                    {
                        q.QImage = File.ReadAllBytes(openFileDialog.FileName);
                    }
                    
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }*/

                //this.QuestionImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        //****************************************************************************************************

        public bool IsTimeValid(string timeStr)
        {
            DateTime time;
            return DateTime.TryParseExact(timeStr, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
        }

        //****************************************************************************************************

        public void setTime(ref DateTime dateTime, string timeString)
        {
            TimeSpan timeSpan = TimeSpan.ParseExact(timeString, "g", CultureInfo.InvariantCulture);
            // parse the time string as a TimeSpan object using the "g" format specifier

            // set the time component of the existing dateTime object to the new time
            dateTime = dateTime.Date + timeSpan;
        }
        //****************************************************************************************************

        private async void examsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (this.examsList.SelectedItem is Exam ex)
            {
             
                QuestionsLB.Items.Clear();
                this.txtExameName.Text = ex.examName;
                this.txtID.Text = ex.examId;
                this.txtTeacher.Text = ex.TeacherName;
                this.exame_Datepicker.SelectedDate = ex.date;
                this.time_begining.Text = ex.BeginTime.ToString("HH:mm");
                this.AnswersListBox.Items.Clear();
                if(QuestionsLB.SelectedItem is Question q)
                {
                    foreach(string ans in q.answers)
                    {
                        this.AnswersListBox.Items.Add(ans);
                    }
                }
                this.IsRandomCB.IsChecked = ex.isRandom;
                this.time_duration.SelectedIndex = (int)(ex.duration*2 -1);
                
             //   this.isNew = false;

                foreach (var question in ex.questions)
                {
                    QuestionsLB.Items.Add(question);
                }
            }
        }
        //****************************************************************************************************

        private async void btn_RemoveExam_Click(object sender, RoutedEventArgs e)
        {
            if (examsList.SelectedItem != null)
            {
                Exam exam = examsList.SelectedItem as Exam;
                //Delete the exam from the detabase
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://localhost:7109/");
                HttpResponseMessage response = await httpClient.DeleteAsync("api/Exams/" + exam.examId);


                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("The Exam: " + exam.examName + "was deleted successfuly");
                    HttpExamRepository.Instance.examList.Remove(exam);
                    examsList.Items.Refresh();

                    

                    //SeachBTN_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Exam was not yet submitted");
                }
            }
        }

        

        private async void submit_Exam_Click(object sender, RoutedEventArgs e)
        {
            Exam exam = new Exam();
            // List<Question> questions = new List<Question>();

            if (QuestionsLB.Items.Count != 0)
            {
                foreach (var question in QuestionsLB.Items)
                {
                    Question q = question as Question;
                    exam.questions.Add(q);
                }
            }
            exam.date = (DateTime)exame_Datepicker.SelectedDate;
            exam.TeacherName = this.txtTeacher.Text;
            exam.examName = this.txtExameName.Text;
            DateTime tempExamBeginTime = exame_Datepicker.SelectedDate.Value;
            setTime(ref tempExamBeginTime, this.time_begining.Text);

            exam.BeginTime = DateTime.Parse(time_begining.Text);
            exam.duration = float.Parse(this.time_duration.SelectedValue.ToString(), CultureInfo.InvariantCulture);
            exam.isRandom = this.IsRandomCB.IsPressed;
            exam.examId = this.txtID.Text;
            

            
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://localhost:7109/");
                StringContent reqBody = new StringContent(JsonSerializer.Serialize(exam),Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("api/Exams", reqBody);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show($" Exam - {exam.examName} has been Created successfuly");
                Close();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                HttpClient httpClientPut = new HttpClient();
                httpClientPut.BaseAddress = new Uri("https://localhost:7109/");
                StringContent reqBody2 = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, "application/json");
                HttpResponseMessage responsePut = await httpClientPut.PutAsync("api/Exams/update/"+exam.examId, reqBody2);
                if(responsePut.StatusCode == System.Net.HttpStatusCode.OK)
                        { 
                            MessageBox.Show($"Exam - {exam.examName} has been Updated successfuly");
                        }
            }
           
        }
        //****************************************************************************************************

        private void time_begining_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsTimeValid(time_begining.Text))
            {
                MessageBox.Show("Exam's begining time needs to be in HH:MM format and valid");
                return;
            }
            DateTime time = exame_Datepicker.SelectedDate.Value;
            setTime(ref time, exame_Datepicker.Text);
        }
        //****************************************************************************************************

        private void QuestionsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  this.Correct_answer.Items.Clear();
            if (QuestionsLB.SelectedItem != null)
            {
                Question selectedQ = QuestionsLB.SelectedItem as Question;

                textQuestion.Text = selectedQ.content;
                /*if (selectedQ.QImage != null)
                {
                    this.QuestionImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(selectedQ.QImage);
                }
*/
                AnswersListBox.Items.Clear();
                int temp = selectedQ.correct;
                this.Correct_answer.Items.Clear();

                foreach (var answer in selectedQ.answers)
                {
                    AnswersListBox.Items.Add(answer);
                    this.Correct_answer.Items.Add(answer);
                                       
                }
                this.Correct_answer.SelectedIndex = temp;
            }
        }


        private void btn_AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            int numberOfQuestion = QuestionsLB.Items.Count + 1;
            Question question = new Question();
            QuestionsLB.Items.Add(question);
            QuestionsLB.SelectedIndex = numberOfQuestion;

            if(examsList.SelectedItem is Exam ex)
            {
                ex.questions.Add(question);
                for (int i = 0; i < ex.questions.Count; i++)
                    {
                        ex.questions[i].index = i+1;
                    
                    }
            }
           
            
        }

        private void time_duration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((this.examsList.SelectedItem is Exam ex) && (this.time_duration.SelectedIndex >0))
            {
                ex.duration = float.Parse( this.time_duration.SelectedValue.ToString(), CultureInfo.InvariantCulture);
            }
        }

        //************************************************************************************************************************
        private void btn_RemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            Question question = new Question();
            question = QuestionsLB.SelectedItem as Question;
            QuestionsLB.Items.Remove(QuestionsLB.SelectedItem);
            QuestionsLB.Items.Refresh();

            if (examsList.SelectedItem is Exam ex)
            {
                ex.questions.Remove(question);
                for (int i = 0; i < ex.questions.Count; i++)
                {
                    ex.questions[i].index = i+1;
                    this.Correct_answer.Items.Add(ex.questions[i]);
                }
            }
        }
        //************************************************************************************************************************
        private void btn_RemoveAnswer_Click(object sender, RoutedEventArgs e)
        {
            AnswersListBox.Items.Remove(AnswersListBox.SelectedItem);
            AnswersListBox.Items.Refresh();

            this.Correct_answer.Items.Clear();
            for (int i = 0; i < this.AnswersListBox.Items.Count; i++)
            {
                this.Correct_answer.Items.Add(AnswersListBox.Items[i]);
            }
        }
        //************************************************************************************************************************
        private void txtExameName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                ex.examName = this.txtExameName.Text;
            }
        }
        //************************************************************************************************************************
        private void IsRandomCB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                ex.isRandom = this.IsRandomCB.IsPressed;
            }
        }
        //************************************************************************************************************************
        private void time_begining_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                if (!IsTimeValid(time_begining.Text))
                {
                    MessageBox.Show("Exam's begining time needs to be in HH:MM format and valid");
                    return;
                }
                DateTime time = exame_Datepicker.SelectedDate.Value;
               // setTime(ref time, exame_Datepicker.Text);
                //   ex.BeginTime = this.time_begining.Text;
            }
        }

        private void textQuestion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(QuestionsLB.SelectedItem is Question question)
            {
                question.content = this.textQuestion.Text;
            }
        }

        private void txtTeacher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                ex.TeacherName = this.txtTeacher.Text;
            }
        }

        private void Correct_answer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Question selectedQ = QuestionsLB.SelectedItem as Question;
            selectedQ.correct = this.Correct_answer.SelectedIndex;
                
        }
        //****************************************************************************************************

    }
}
