using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mvvm.Model;
using mvvm.ViewModel.Commnad;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using mvvm.View;

namespace mvvm.ViewModel
{
    public class SpaceConfigurationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// przechowywana aktualnie mapa
        /// </summary>
        private Map myMap;
        public Map MyMap 
        {
            get { return myMap; }
            set
            {
                myMap = value;
                OnPropertyChanged("MyMap");
            }
        }
        /// <summary>
        /// przepisana z Mapy lista kinectow
        /// </summary>
        private ObservableCollection<MKinect> MKinectList;// { get; set; }
        public ObservableCollection<MKinect> mKinectList
        {
            get { return MKinectList; }
            set
            {
                MKinectList = value;
                OnPropertyChanged("mKinectList");
            }
        }
        
        /// <summary>
        /// aktualnie wybrany kinect
        /// </summary>
        private int selectedMKinectIndex;
        public int SelectedMKinectIndex
        {
            get 
            {
                //MyMap.KinectList[selectedMKinectIndex].X = CurrentKinectX;
                //MyMap.KinectList[selectedMKinectIndex].Y = CurrentKinectY;
                if (selectedMKinectIndex >= 0)
                {
                    CurrentKinectX = MyMap.KinectList[selectedMKinectIndex].X;
                    CurrentKinectY = MyMap.KinectList[selectedMKinectIndex].Y;
                    CurrentKinectAngle = MyMap.KinectList[selectedMKinectIndex].Angle;
                }
                return selectedMKinectIndex; 
            }
            set
            {
                selectedMKinectIndex = value;
                //if (selectedMKinectIndex > 0)
                //{
                //    CurrentKinectX = MyMap.KinectList[value].X;
                //    CurrentKinectY = MyMap.KinectList[value].Y;
                //}
                OnPropertyChanged("SelectedMKinectIndex");
            }
        }

        /// <summary>
        /// szerokosc oraz dlugosc mapy
        /// narazie nie jest aktualizowane w mapie
        /// </summary>
        private int recHeight;
        public int RecHeight
        {
            get { return recHeight; }
            set
            {
                recHeight = value;
                OnPropertyChanged("RecHeight");
            }
        }

        private int recWidth;
        public int RecWidth
        {
            get { return recWidth; }
            set
            {
                recWidth = value;
                OnPropertyChanged("RecWidth");
            }
        }

        //private MKinect selectedMKinect;
        //public MKinect SelectedMKinect
        //{
        //    get { return selectedMKinect; }
        //    set 
        //    {
        //        selectedMKinect = value;
        //        OnPropertyChanged("SelectedMKinect");
        //    }
        //}
        /// <summary>
        /// aktualna pozycja X wybranego kinecta
        /// </summary>
        private double currentKinectX;
        public double CurrentKinectX
        {
            get { return currentKinectX; }
            set
            {
                currentKinectX = value;
                OnPropertyChanged("CurrentKinectX");
            }
        }
        /// <summary>
        /// aktualna pozycja Y wybranego kinecta
        /// </summary>
        private double currentKinectY;
        public double CurrentKinectY
        {
            get { return currentKinectY; }
            set
            {
                currentKinectY = value;
                OnPropertyChanged("CurrentKinectY");
            }
        }
        /// <summary>
        /// aktualne obrot
        /// </summary>
        private double currentRotationX;
        public double CurrentRotationX
        {
            get { return currentKinectX; }
            set
            {
                currentKinectX = value;
                OnPropertyChanged("CurrentRotationX");
            }
        }

        private double currentRotationY;
        public double CurrentRotationY
        {
            get { return currentKinectY; }
            set
            {
                currentKinectY = value;
                OnPropertyChanged("CurrentRotationY");
            }
        }


        private double currentKinectAngle;
        public double CurrentKinectAngle
        {
            get { return currentKinectAngle; }
            set
            {
                currentKinectAngle = value;
                OnPropertyChanged("CurrentKinectAngle");
            }
        }

        private int selectedMode;
        public int SelectedMode
        {
            get 
            {
                return MyMap.MapMode-1; 
            }
            set
            {
                selectedMode = value;
                MyMap.MapMode = value+1;
                OnPropertyChanged("SelectedMode");
            }
        }

        /// <summary>
        /// przypisanie wymiarow mapie
        /// </summary>
        public DelegateCommand SetDimensionCommand { get; set; }
        public DelegateCommand SetCurrentKinectPositionCommand { get; set; }
        /// <summary>
        /// przypisanie mapy do lokalnego pola MyMap
        /// zainicjalizowanie kolekcji obserwowanej lista kinectow
        /// ustawienie wartosci domyslnej planszy
        /// </summary>
        /// <param name="m"></param>
        public SpaceConfigurationViewModel(Map m)
        {
            MyMap = m;
            mKinectList = new ObservableCollection<MKinect>(MyMap.KinectList);
            RecHeight = (int)MyMap.DimensionY;
            RecWidth = (int)MyMap.DimensionX;
            SelectedMKinectIndex = 0;

            SetDimensionCommand = new DelegateCommand(SetDimensionAction, SetDimensionCanExecute);
            SetCurrentKinectPositionCommand = new DelegateCommand(SetCurrentKinectPositionAction, SetCurrentKinectPositionCanExecute);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// zapisanie do mapy wymiarow ustawionych przez uzytkownika
        /// </summary>
        /// <param name="obj"></param>
        private void SetDimensionAction(object obj)
        {
            MyMap.setDimension(RecWidth, RecHeight);
        }

        private bool SetDimensionCanExecute(object obj)
        {
            return true;
        }

        /// <summary>
        /// ustaw pozycje aktualnie wybranego kinecta
        /// </summary>
        /// <param name="obj"></param>
        private void SetCurrentKinectPositionAction(object obj)
        {
            List<MKinect> tmp = new List<MKinect>(MyMap.KinectList);
            //MyMap.KinectList.Clear();
            tmp[selectedMKinectIndex].setPosition(CurrentKinectX, CurrentKinectY, CurrentKinectAngle);
            //MyMap.KinectList[SelectedMKinectIndex].setPosition(CurrentKinectX, CurrentKinectY, CurrentKinectAngle);
            MyMap.KinectList = tmp;
            mKinectList = new ObservableCollection<MKinect>(MyMap.KinectList);
            //chwilowo zmieniam indeks nastepnie spowrotem ustawiam stara jego wartosc
            //powoduje to automatyczne odswiezenie wartosci
            int tmpInd = SelectedMKinectIndex;
            SelectedMKinectIndex = -1;
            SelectedMKinectIndex = tmpInd;
        }
        private bool SetCurrentKinectPositionCanExecute(object obj)
        {
            return true;
        }
    }
}
