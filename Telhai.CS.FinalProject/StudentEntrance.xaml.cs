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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telhai.CS.APIServer.Models;

namespace Telhai.CS.FinalProject
{
    /// <summary>
    /// Interaction logic for StudentEntrance.xaml
    /// </summary>
    public partial class StudentEntrance : Window
    {
        public StudentEntrance()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded_1;
            examsList.ItemsSource = HttpExamRepository.Instance.examList;
        }

        
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



        private void btn_TakeExam_Click(object sender, RoutedEventArgs e)
        {
            if (this.examsList.SelectedItem is Exam ex)
            {
                Test test = new Test();
            }
        }

        private void examsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
