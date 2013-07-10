﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace mvvm.Model
{
    public class Path : INotifyPropertyChanged
    {
        /// <summary>
        /// sciezka w ktorej przetrzymywana bedzie droga pierwszego uzytkownika
        /// </summary>
        private List<MConstruction> trackPath;
        public List<MConstruction> TrackPath 
        {
            get { return trackPath; }
            set
            {
                trackPath = value;
                OnPropertyChanged("TrackPath");
            }
        }

        /// <summary>
        /// przochuje pierwszy z listy path
        /// wykorzystywane jest przy powtarzaniu sciezki
        /// </summary>
        private MConstruction nextToGet;
        public MConstruction NextToGet
        {
            get { return nextToGet; }
            set
            {
                nextToGet = value;
                OnPropertyChanged("NextToGet");
            }
        }


        private readonly BackgroundWorker createPathWorker = new BackgroundWorker();
        private readonly BackgroundWorker repeatPathWorker = new BackgroundWorker();

        private Map MyMap;
        public Path()
        {
            TrackPath = new List<MConstruction>();
            NextToGet = new MConstruction("", -100, -100);
        }

        public bool isBusyCreateNewPath()
        {
            if (createPathWorker.IsBusy == true)
                return true;
            return false;
        }

        public void createNewPath(Map m)
        {
            MyMap = m;
            createPathWorker.WorkerSupportsCancellation = true;
            createPathWorker.DoWork += createPathWorker_DoWork;
            createPathWorker.RunWorkerAsync();
        }

        private void createPathWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TrackPath.Clear();
            while (true)
            {
                if (MyMap.UserY != -100 && MyMap.UserX != -100)
                {
                    TrackPath.Add(new MConstruction("", MyMap.UserX, MyMap.UserY));
                    break;
                }
            }

            while (true)
            {
                if (createPathWorker.CancellationPending)
                {
                    e.Cancel = true;
                    if (TrackPath != null)
                    {
                        
                        
                        NextToGet = TrackPath[0];
                    }
                    return;
                }
                if(Calculations.calculateDistance((TrackPath[TrackPath.Count-1].X), (TrackPath[TrackPath.Count-1]).Y, MyMap.UserX, MyMap.UserY) > 20)
                {
                    List<MConstruction> tmp = new List<MConstruction>(TrackPath);
                    tmp.Add(new MConstruction("", MyMap.UserX, MyMap.UserY));

                    TrackPath = new List<MConstruction>(tmp);
                }
                Thread.Sleep(500);
            }
        }

        public void stopRecordPath()
        {
            createPathWorker.CancelAsync();
        }


        public void clearFirst()
        {

            if (TrackPath.Count > 1)
            {
                TrackPath.RemoveAt(0);
                NextToGet = TrackPath[0];
            }
            else
            {
                NextToGet = new MConstruction("", -100, -100);
            }
        }

        public void repeatPath(Map m)
        {
            MyMap = m;
            repeatPathWorker.DoWork += repeatPathWorker_DoWork;
            repeatPathWorker.RunWorkerAsync();

            //clearFirst();

            //while(true)
            //{
            //    if(Calculations.calculateDistance(NextToGet.X, NextToGet.Y, MyMap.UserX, MyMap.UserY)<50)
            //    {
            //        clearFirst();
            //    }
            //    Thread.Sleep(500);
            //}
        }

        private void repeatPathWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (TrackPath.Count == 1)
                {
                    TrackPath.Clear();
                    NextToGet = new MConstruction("", -100, -100);
                    return;
                }
                if (Calculations.calculateDistance(NextToGet.X, NextToGet.Y, MyMap.UserX, MyMap.UserY) < 20)
                {
                    clearFirst();
                }
                else
                {
                    //navigate to point
                    Calculations.navigateToPoint(MyMap.UserX, MyMap.UserY, NextToGet.X, NextToGet.Y);
                }
                Thread.Sleep(1500);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
