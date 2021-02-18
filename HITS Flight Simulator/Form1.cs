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
        public Form1()
        {
            InitializeComponent();
        }

        double dt;
        const double g = 9.8;
        const double C = 0.15;
        const double rho = 1.29;

        double a;
        double v0;
        double y0;
        double S;
        double m;
        double k;

        double t;
        double x;
        double y;
        double vx;
        double vy;
        double time_sum;
        private void button1_Click(object sender, EventArgs e)
        {
            time_sum = 0;
            time_timer.Text = $"Времени прошло {time_sum}";
            a = (double)edAngle.Value * Math.PI / 180;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;
            m = (double)edWeight.Value;
            S = (double)edSquare.Value;
            k = 0.5 * C * S * rho / m;
            dt = (double)timer1.Interval / 1000;

            vx = v0 * Math.Cos(a);
            vy = v0 * Math.Sin(a);


            t = 0;
            x = 0;
            y = y0;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);
            timer1.Start();
            double max_height = v0 * v0 * Math.Sin(a) * Math.Sin(a) / (2 * g) + 2;
            double max_lenght = v0 * v0 * Math.Sin(2 * a) / g + 2;
            chart1.ChartAreas[0].AxisY.Maximum = max_height;
            chart1.ChartAreas[0].AxisX.Maximum = max_lenght;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time_sum += dt;
            time_timer.Text = $"Времени прошло {time_sum} мс";
            t += dt;
            vx = vx - k * vx * Math.Sqrt(vx * vx + vy * vy) * dt;
            vy = vy - (g + k * vy * Math.Sqrt(vx * vx + vy * vy)) * dt;
            
            x = x + vx * dt;
            y = y + vy * dt;

            chart1.Series[0].Points.AddXY(x, y);
            if (y <= 0) timer1.Stop();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button2.Text = "Continue";
            }
            else if (!timer1.Enabled && y > 0)
            {
                timer1.Start();
                button2.Text = "Stop";
            }
        }
    }
}
