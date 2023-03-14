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
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
       /*
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as UserViewModel;
            viewModel.AlarmStatus = "On";
        }
       */
        public ExamWindow()
        {
            InitializeComponent();
        }
    }
}
