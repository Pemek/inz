using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.ComponentModel;
using System.Threading;


namespace mvvm.Model
{
    [Serializable]
    public class MKinect : INotifyPropertyChanged
    {
        

        public string Number { get; set; }

        public string DevianceId { get; set; }
        
        public double X { get; set; }
        public double Y { get; set; }
        public double Angle { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public double GetDrawX { get; set; }
        public double GetDrawY { get; set; }
        public double GetLineX { get; set; }

        private double currentUserX;
        public double CurrentUserX 
        {
            get { return currentUserX; }
            set
            {
                currentUserX = value;
                OnPropertyChanged("CurrentUserX");
            }
        }
        private double currentUserY;
        public double CurrentUserY
        {
            get { return currentUserY; }
            set
            {
                currentUserY = value;
                OnPropertyChanged("CurrentUserY");
            }
        }

        /// <summary>
        /// sensor kinecta przechowywany w klasie
        /// </summary>
        [NonSerialized]
        private KinectSensor myKinectSensor;
        public KinectSensor MyKinectSensor 
        {
            get { return myKinectSensor; }
            set
            {
                myKinectSensor = value;
                OnPropertyChanged("MyKinectSensor");
            }
        }

        public MKinect(string n, KinectSensor ks)
        {
            Number = n;
            //DevianceId = id;
            MyKinectSensor = ks;
            X = 0;
            Y = 0;
            
            Angle = 0;

            Width = 50;
            Height = 50;
            GetDrawX = X - Width/2;
            GetDrawY = Y - Height/2;
            GetLineX = GetDrawX + 30;

            CurrentUserX = X + 50;
            CurrentUserY = Y + 50;

        }

        public void setPosition(double x, double y, double a)
        {
            
            X = x;
            Y = y;
            GetLineX = X + 30;
            GetDrawX = x - Width/2;
            GetDrawY = y - Height/2;
            Angle = a;
            GetLineX = GetDrawX + 30;
        }

        public void setUserPosition(double x, double y)
        {
            CurrentUserX = new Translation().calculateX(new Rotation().calculateX(x, y, Angle), X);
            CurrentUserY = new Translation().calculateY(new Rotation().calculateY(x, y, Angle), Y);
        }

        #region
            [field: NonSerialized]
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }

            [field: NonSerialized]
            private readonly BackgroundWorker kinectMoveWorker = new BackgroundWorker();
            private void kinectMoveWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                MyKinectSensor.ElevationAngle = 27;
                Thread.Sleep(1000);
                MyKinectSensor.ElevationAngle = -27;
                Thread.Sleep(1000);
                MyKinectSensor.ElevationAngle = 0;
            }
        #endregion

        public void moveSensor()
        {
            if (!kinectMoveWorker.IsBusy)
            {
                kinectMoveWorker.DoWork += kinectMoveWorker_DoWork;
                kinectMoveWorker.RunWorkerAsync();
            }
            //MyKinectSensor.ElevationAngle = 27;
            //Thread.Sleep(1000);
            //MyKinectSensor.ElevationAngle = -27;
            //Thread.Sleep(1000);
            //MyKinectSensor.ElevationAngle = 0;
        }

        public MKinect copyKinectList(MKinect mKinect)
        {
            MyKinectSensor = mKinect.MyKinectSensor;
            return this;
        }

        
    }
}
