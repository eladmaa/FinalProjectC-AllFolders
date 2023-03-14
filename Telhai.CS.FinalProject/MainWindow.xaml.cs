using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Telhai.CS.FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void std_Click(object sender, RoutedEventArgs e)
        {
            StudentEntrance student = new StudentEntrance();
            student.Show();
        }

        private void tchr_Click(object sender, RoutedEventArgs e)
        {
            TeacherWindow Teacher= new TeacherWindow();
            Teacher.Show();
        //    List<Exam> exams = Teacher.allExams();
        }
    }
}
