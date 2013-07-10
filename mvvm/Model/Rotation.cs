using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace mvvm.Model
{
    class Rotation
    {
        public Rotation()
        {
            
        }
        public double calculateX(double x, double y, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return x * Math.Cos(angle * Math.PI / 180) - y * Math.Sin(angle * Math.PI / 180);
        }
        public double calculateY(double x, double y, double angle)
        {
            return x * Math.Sin(angle * Math.PI / 180) + y * Math.Cos(angle * Math.PI / 180);
        }
    }
}
