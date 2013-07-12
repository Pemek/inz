using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mvvm.Model;
using Microsoft.Win32;
using mvvm.ViewModel.Commnad;
using System.Windows;

namespace mvvm.ViewModel
{
    public class Mode2ViewModel : INotifyPropertyChanged
    {
        public int Flag { get; set; }
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

        private Instruments myInstruments;
        public Instruments MyInstruments
        {
            get { return myInstruments; }
            set
            {
                myInstruments = value;
                OnPropertyChanged("MyInstruments");
            }
        }

        public DelegateCommand PlayMusicCommand { get; set; }

        public Mode2ViewModel(Map m)
        {
            MyMap = m;

            openMap();
            if (Flag == 1)
            {
                KinectInit.MyMap = m;
                KinectInit.initConfigurationMode();
            }

            MyInstruments = new Instruments(MyMap);

            PlayMusicCommand = new DelegateCommand(PlayMusicAction);


        }

        


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

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
                    if (tmp.MapMode == 2)
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

        private void PlayMusicAction(object obj)
        {
            MyInstruments.playMusic();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            // Handle closing logic, set e.Cancel as needed
            MyInstruments.stopMusic();
            //KinectInit.kinectStop();
        }
    }
}
