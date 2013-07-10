using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Kinect;

namespace mvvm.Model
{
    [Serializable]
    public class Map : INotifyPropertyChanged
    {
            


        public string MapName { get; set; }
        public double DimensionX { get; set; }
        public double DimensionY { get; set; }
        private int mapMode;
        public int MapMode 
        {
            get { return mapMode; }
            set
            {
                mapMode = value;
                OnPropertyChanged("MapMode");
            }
        }

        private double userX;
        public double UserX 
        {
            get { return userX; }
            set
            {
                userX = value;
                OnPropertyChanged("UserX");
            }
        }
        private double userY;
        public double UserY 
        {
            get { return userY; }
            set
            {
                userY = value;
                OnPropertyChanged("UserY");
            }
        }

        private List<MKinect> kinectList;
        public List<MKinect> KinectList 
        {
            get { return kinectList; }
            set
            {
                kinectList = value;
            }
        }
        private List<MConstruction> constructionList;
        public List<MConstruction> ConstructionList
        {
            get { return constructionList; }
            set
            {
                constructionList = value;
                OnPropertyChanged("ConstructionList");
            }
        }


        public Map()
        {
        }
        public Map(string name)
        {
            MapName = name;
            KinectList = new List<MKinect>();
            ConstructionList = new List<MConstruction>();
            MapMode = 1;

            kinectInitWorker.DoWork += kinectInitWorker_DoWork;
        }

        public void setDimension(double x, double y)
        {
            DimensionX = x;
            DimensionY = y;
        }


        #region
            [field:NonSerialized]
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }

            [field: NonSerialized]
            private readonly BackgroundWorker kinectInitWorker = new BackgroundWorker();
            /// <summary>
            /// inicjalizacja kinecta w tle
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void kinectInitWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                KinectList.Clear();
                foreach (var potentialSensor in KinectSensor.KinectSensors)
                {
                    if (potentialSensor.Status == KinectStatus.Connected && findKinect(potentialSensor.UniqueKinectId) == -1)
                    {
                        KinectList.Add(new MKinect((KinectList.Count + 1).ToString(), potentialSensor));
                    }
                }
                foreach (var potentialMSensor in KinectList)
                {
                    potentialMSensor.MyKinectSensor.SkeletonStream.Enable();

                    try
                    {
                        potentialMSensor.MyKinectSensor.Start();
                    }
                    catch
                    {
                    }
                }
            }
        #endregion

        public void addConstruction(MConstruction construction)
        {
            List<MConstruction> tmp = new List<MConstruction>(ConstructionList);
            tmp.Add(construction);
            ConstructionList.Clear();
            ConstructionList = tmp;
        }
        

        public void setUserPosition(int index, double x, double y)
        {
            KinectList[index].setUserPosition(x, y);

            OnPropertyChanged("KinectList");
        }

        public int findKinect(string unqId)
        {
            for(int i=0; i<KinectList.Count; i++)
            {
                if(KinectList[i].MyKinectSensor.UniqueKinectId.Equals(unqId))
                {
                    return i;
                }
            }
            return -1;
        }

        public void kinectSensorInit()
        {
            kinectInitWorker.RunWorkerAsync();

            //proba odpalenia w tle
            //foreach (var potentialSensor in KinectSensor.KinectSensors)
            //{
            //    if (potentialSensor.Status == KinectStatus.Connected)
            //    {
            //        KinectList.Add(new MKinect((KinectList.Count + 1).ToString(), potentialSensor));
            //    }
            //}
            //foreach (var potentialMSensor in KinectList)
            //{
            //    potentialMSensor.MyKinectSensor.SkeletonStream.Enable();

            //    try
            //    {
            //        potentialMSensor.MyKinectSensor.Start();
            //    }
            //    catch
            //    {
            //    }
            //}
        }

        public void moveSensor(int index)
        {
            if(KinectList != null)
                KinectList[index].moveSensor();
        }

        /// <summary>
        /// wypelnia mape podanym argumentem
        /// bez zmian zostaje jedynie MKinect.KinectSensor
        /// </summary>
        /// <param name="m"></param>
        public void mapCopyFrom(Map m)
        {
            MapName = m.MapName;
            DimensionX = m.DimensionX;
            DimensionY = m.DimensionY;
            for (int i = 0; i < KinectList.Count; i++)
            {
                KinectList[i] = m.KinectList[i].copyKinectList(KinectList[i]);
            }
            ConstructionList = new List<MConstruction>(m.ConstructionList);
            MapMode = m.MapMode;
        }

        public void placeNewConstruction()
        {
            addConstruction(new MConstruction((ConstructionList.Count + 1).ToString(), UserX, UserY));
            //ConstructionList.Add(new MConstruction((ConstructionList.Count+1).ToString(), UserX, UserY));
        }

        public void removeConstruction(int index)
        {
            List<MConstruction> tmp = new List<MConstruction>(ConstructionList);
            tmp.RemoveAt(index);
            ConstructionList.Clear();
            ConstructionList = tmp;
        }


        
    }
}
