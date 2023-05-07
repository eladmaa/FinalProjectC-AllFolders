using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Telhai.CS.FinalProject
{
    public partial class Form1 : Form
    {
        private float _timeRemaining;
        public event EventHandler TimerFinished;
        public Form1(float time)
        {
            InitializeComponent();
            _timeRemaining = time;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Tick += timer1_Tick;

            // Set time remaining to 1 hour
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            // Decrease time remaining by 1 second
            _timeRemaining -= 1;

            // Calculate hours, minutes, and seconds
            int hours = (int)(_timeRemaining / 3600);
            int minutes = (int)(_timeRemaining % 3600 / 60);
            int seconds = (int)(_timeRemaining % 60);

            // Display time remaining in label
            label1.Text = hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");

            // Stop timer when time runs out
            if (_timeRemaining <= 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("Time's up!");
                TimerFinished?.Invoke(this, EventArgs.Empty);

                return;
            }
        }

    }
}
