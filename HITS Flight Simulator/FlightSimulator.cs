using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HITS_Flight_Simulator
{
    public class FlightSimulator
    {
        double dt;
        const double g = 9.8;
        const double C = 0.15;
        const double rho = 1.29;

        public double a;
        public double v0;
        public double y0;
        public double S;
        public double m;
        public double k;

        double t;
        double x;
        public double y;
        double vx;
        double vy;
        public double time_sum =0;

      
        public (double, double) TickOfFlight()
        {
            time_sum += dt;

            t += dt;
            vx = vx - k * vx * Math.Sqrt(vx * vx + vy * vy) * dt;
            vy = vy - (g + k * vy * Math.Sqrt(vx * vx + vy * vy)) * dt;

            x = x + vx * dt;
            y = y + vy * dt;

            return (x, y);
        }

        public (double,double) MaxHL()
        {

            return(v0 *v0*  Math.Sin(a) * Math.Sin(a) / (2 * g) + 2+y0, v0 *v0 * Math.Sin(2 * a) / g + 2);
        }

        public void StartOfFlight(double dtime )
        {
            time_sum = 0;
            k = 0.5 * C * S * rho / m;
            dt = dtime;
            vx = v0 * Math.Cos(a);
            vy = v0 * Math.Sin(a);
            t = 0;
            x = 0;
            y = y0;
        }
    }
}
