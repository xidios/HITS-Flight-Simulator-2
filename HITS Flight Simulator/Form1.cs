using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HITS_Flight_Simulator
{
    public partial class Form1 : Form
    {
        FlightSimulator fg = new FlightSimulator();
        public Form1()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            time_timer.Text = $"Времени прошло {fg.time_sum}";
            InitFlightComp();
            fg.StartOfFlight((double)timer1.Interval / 1000);
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, fg.y0);
            timer1.Start();
            double max_height = fg.MaxHL().Item1;
            double max_lenght = fg.MaxHL().Item2;
            chart1.ChartAreas[0].AxisY.Maximum = max_height;
            chart1.ChartAreas[0].AxisX.Maximum = max_lenght;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            (double,double) XY = fg.TickOfFlight();
            time_timer.Text = $"Времени прошло {fg.time_sum}";
            chart1.Series[0].Points.AddXY(XY.Item1, XY.Item2);
            if (XY.Item2 <= 0) timer1.Stop();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button2.Text = "Continue";
            }
            else if (!timer1.Enabled && fg.y > 0)
            {
                timer1.Start();
                button2.Text = "Stop";
            }
        }
        private void InitFlightComp()
        {
            fg.a = (double)edAngle.Value * Math.PI / 180;
            fg.v0 = (double)edSpeed.Value;
            fg.y0 = (double)edHeight.Value;
            fg.m = (double)edWeight.Value;
            fg.S = (double)edSquare.Value;
        }
    }
}
