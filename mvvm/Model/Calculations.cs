using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mvvm.Model.NAudio;

namespace mvvm.Model
{
    public class Calculations
    {
        public static double calculateDistance(double x1, double y1, double x2, double y2)
        {
            double t1, t2, t3;
            t1 = Math.Pow((x2 - x1), 2);
            t2 = Math.Pow((y2 - y1), 2);
            t3 = Math.Sqrt(t1 + t2);

            return t3;
        }

        public static void navigateToPoint(double fromX, double fromY, double toX, double toY)
        {
            if (toX - fromX > 20)
            {
                //navigate right
                System.Diagnostics.Debug.WriteLine("right");
                SoundPlayer.playRight();
            }
            else if (fromX - toX > 20)
            {
                //navigate left
                System.Diagnostics.Debug.WriteLine("left");
                SoundPlayer.playLeft();
            }
            else if (toY - fromY > 20)
            {
                //navigate down
                System.Diagnostics.Debug.WriteLine("down");
                SoundPlayer.playDown();
            }
            else if (fromY - toY > 20)
            {
                //navigate up
                System.Diagnostics.Debug.WriteLine("up");
                SoundPlayer.playUp();
            }

        }
    }
}
