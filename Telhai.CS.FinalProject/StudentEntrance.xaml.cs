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
using System.Windows.Shapes;

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
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            // TBD search DB for the exam and if date and time are with range the exam window will open
            ExamWindow exam= new ExamWindow();
            exam.Show();
            List<Question> questionList = new List<Question>();

        }
    }
}
