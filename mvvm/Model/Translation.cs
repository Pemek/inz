using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mvvm.Model
{
    class Translation
    {
        public double calculateX(double x, double kinectx)
        {
            return x+kinectx;
        }

        public double calculateY(double y, double kinecty)
        {
            return y+kinecty;
        }
    }
}
