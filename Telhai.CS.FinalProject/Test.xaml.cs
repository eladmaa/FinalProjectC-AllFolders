using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using ZstdSharp.Unsafe;

namespace Telhai.CS.FinalProject
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        Exam exam;
        public Test(Exam ex)
        {
            InitializeComponent();
            this.exam = new Exam();
            this.exam = ex;
        }

        
        private void submit_Exam_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuestionsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
