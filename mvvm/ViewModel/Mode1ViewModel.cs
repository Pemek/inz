using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Win32;
using mvvm.Model;
using mvvm.ViewModel.Commnad;
using System.Windows;

namespace mvvm.ViewModel
{
    public class Mode1ViewModel : INotifyPropertyChanged
    {
        public int Flag { get; set; }
        public Map MyMap { get; set; }
        private Path myPath;
        public Path MyPath 
        {
            get { return myPath; }
            set
            {
                myPath = value;
                OnPropertyChanged("MyPath");
            }
        }

        public DelegateCommand RecordCommand { get; set; }
        public DelegateCommand StopRecordCommand { get; set; }
        public DelegateCommand StartRepeatCommand { get; set; }
        public DelegateCommand StopRepeatCommand { get; set; }

        public Mode1ViewModel(Map m)
        {
            MyMap = m;
            //utworzenie nowej czystej sciezki
            MyPath = new Path();

            openMap();
            if (Flag == 1)
            {
                KinectInit.MyMap = m;
                KinectInit.initConfigurationMode();
            }


            IsEnabledStartRecord = true;
            IsEnabledStopRecord = false;
            IsEnabledStartRepeat = false;


            RecordCommand = new DelegateCommand(RecordAction, RecordCanExecute);
            StopRecordCommand = new DelegateCommand(StopRecordAction, StopRecordCanExecute);
            StartRepeatCommand = new DelegateCommand(StartRepeatAction, StartRepeatCanExecute);
            StopRepeatCommand = new DelegateCommand(StopRepeatAction, StopRepeatCanExecute);
        }


        private bool isEnabledStartRecord;
        public bool IsEnabledStartRecord 
        {
            get { return isEnabledStartRecord; }
            set
            {
                isEnabledStartRecord = value;
                OnPropertyChanged("IsEnabledStartRecord");
            }
        }

        private bool isEnabledStopRecord;
        public bool IsEnabledStopRecord 
        {
            get { return isEnabledStopRecord; }
            set
            {
                isEnabledStopRecord = value;
                OnPropertyChanged("IsEnabledStopRecord");
            }
        }

        private bool isEnabledStartRepeat;
        public bool IsEnabledStartRepeat
        {
            get { return isEnabledStartRepeat; }
            set
            {
                isEnabledStartRepeat = value;
                OnPropertyChanged("IsEnabledStartRepeat");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// otwiera mape
        /// sprawdza czy zgadza sie liczba kinectow
        /// oraz typ mapy
        /// </summary>
        private void openMap()
        {
            Serializer ser = new Serializer();
            //MyMap = ser.DeSerializeObject("ala");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                Map tmp = ser.DeSerializeObject(openFileDialog.FileName);
                if (MyMap.KinectList.Count == tmp.KinectList.Count)
                {
                    if (tmp.MapMode == 1)
                    {
                        MyMap.mapCopyFrom(tmp);
                        Flag = 1;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Wybrany tryb rozgrywki oraz mapy roznia sie!");
                    }
                }
                else
                {
                    MessageBox.Show("Mapa stworzona dla " + tmp.KinectList.Count + " Kinectow, a podlaczonych jest " + MyMap.KinectList.Count);
                }
            }

            Flag = 0;

        }

        /// <summary>
        /// rozpoczecie nagrywania
        /// </summary>
        /// <param name="obj"></param>
        private void RecordAction(object obj)
        {
            MyPath.createNewPath(MyMap);

            IsEnabledStartRecord = false;
            IsEnabledStopRecord = true;
            IsEnabledStartRepeat = false;
        }

        private bool RecordCanExecute(object obj)
        {
            //if (MyPath.isBusyCreateNewPath())
            //{
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// zatrzymanie nagrywania
        /// </summary>
        /// <param name="obj"></param>
        private void StopRecordAction(object obj)
        {
            MyPath.stopRecordPath();

            IsEnabledStartRecord = true;
            IsEnabledStopRecord = false;
            IsEnabledStartRepeat = true;
            
        }
        private bool StopRecordCanExecute(object obj)
        {
            //if (!MyPath.isBusyCreateNewPath())
            //{
            //    return false;
            //}
            return true;
        }

        private void StartRepeatAction(object obj)
        {
            MyPath.repeatPath(MyMap);
            //MyPath.clearFirst();

            //IsEnabledStartRecord = false;
            //IsEnabledStartRepeat = false;
            //isEnabledStopRecord = false;
        }

        private bool StartRepeatCanExecute(object obj)
        {
            //if (MyPath.NextToGet == null)
            //    return false;
            return true;
        }

        private void StopRepeatAction(object boj)
        {
        }

        private bool StopRepeatCanExecute(object obj)
        {
            return true;
        }
    }
}
