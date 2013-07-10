using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.ComponentModel;
using System.Threading;

namespace mvvm.Model
{
    public class KinectInit
    {
        /// <summary>
        /// watek pracujacy w tle inicjalizuje on sensory
        /// </summary>
        private readonly BackgroundWorker kinectInitWorker = new BackgroundWorker();

        //private static Map myMap;
        //public static Map MyMap
        //{
        //    get { return myMap; }
        //    set
        //    {
        //        myMap = value;
        //    }
        //}

        public static Map MyMap = new Map();

        //public void moveSensor(int index)
        //{
        //    KinectList[index].move();
        //}


        //private void addKinect(KinectSensor ks, int nr)
        //{
        //    List<MSensor> tmp = new List<MSensor>(KinectList);
        //    KinectList.Clear();
        //    tmp.Add(new MSensor(ks, nr));
        //    KinectList = new List<MSensor>(tmp);
        //}

        private void kinectInitWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //foreach (var potentialSensor in KinectSensor.KinectSensors)
            //{
            //    if (potentialSensor.Status == KinectStatus.Connected)
            //    {
            //        addKinect(potentialSensor, KinectList.Count + 1);
            //    }
            //}
            //foreach (var potentialMSensor in KinectList)
            //{
            //    potentialMSensor.Sensor.SkeletonStream.Enable();

            //    try
            //    {
            //        potentialMSensor.Sensor.Start();
            //    }
            //    catch
            //    {
            //    }
            //}
        }

        /// <summary>
        /// rozpoczyna tryb konfiguracji przestrzeni
        /// </summary>
        public static void initConfigurationMode()
        {
            foreach (var potentialSensor in MyMap.KinectList)
            {
                potentialSensor.MyKinectSensor.SkeletonFrameReady += skelFrameReady;
            }
        }


        /// <summary>
        /// inicjalizacja odczytu funkcji
        /// </summary>
        public static void initConfigurationModeStop()
        {
            foreach (var potentialSensor in MyMap.KinectList)
            {
                potentialSensor.MyKinectSensor.SkeletonFrameReady -= skelFrameReady;
            }
        }

        /// <summary>
        /// funkcja wykonywana po kazdym odczycie z kinecta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void skelFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            KinectSensor ks = (KinectSensor)sender;

            int index = MyMap.findKinect(ks.UniqueKinectId);
            //jezeli chociaz jeden przeslany szkielet byl niezerowy
            //to nie wejdzie w ostatniego ifa
            int flaga = 0;
            
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
                    //jezeli nie ma zadnej postaci sledzonej to wstawiam 2 ujemne wartosci
                    //nie ma mozliwosci zeby kinect cos takiego wygenerowal wiec wiem ze to bledny odczyt
            }
            foreach (Skeleton skel in skeletons)
            {
                if (skel.Position.X != 0 && skel.Position.Z !=0 && skel.Position.Y != 0)
                {
                    flaga++;
                    MyMap.setUserPosition(index, scale(skel.Position.Z), scale(-skel.Position.X));

                }
                
            }
            if(flaga==0)
                MyMap.setUserPosition(index, 0, 0);
            //MyMap.setUserPosition(0, 100, 100);
            CalculateUserPosition();
        }

        /// <summary>
        /// sklalowanie wyniku pobranego z kinecta
        /// </summary>
        /// <param name="wynik"></param>
        /// <returns></returns>
        private static double scale(double wynik)
        {
            int scale = 100;
            return wynik * scale;
        }

        /// <summary>
        /// wyliczenie pozycji uzytkownika na podstawie sredniej wazonej
        /// </summary>
        /// <returns></returns>
        public static void CalculateUserPosition()
        {
            int i = 0;
            double xSum=0, ySum=0;

            foreach(var kinect in MyMap.KinectList)
            {
                if (kinect.CurrentUserX != kinect.X && kinect.CurrentUserY != kinect.Y)
                {
                    i++;
                    xSum += kinect.CurrentUserX;
                    ySum += kinect.CurrentUserY;

                }
                //kinect.CurrentUserX 
            }
            //gdy zaden z kinectow nie widzi obrazu
            if (i == 0)
            {
                MyMap.UserX = -100;
                MyMap.UserY = -100;
                return;
            }

            MyMap.UserX = xSum / i;
            MyMap.UserY = ySum / i;
        }


        public static void kinectStop()
        {
            foreach (var potentialSensor in MyMap.KinectList)
            {
                potentialSensor.MyKinectSensor.Stop();
            }
        }
    }

}
