using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Kinect;
using mvvm.Model;
using mvvm.ViewModel.Commnad;

namespace mvvm.ViewModel
{
    public class KinectConfigurationViewModel : INotifyPropertyChanged
    {
        public string Text1 { get; set; }

        public DelegateCommand ShowKinectCommand { get; set; }
        public Map MyMap { get; set; }

        private int mKinectIndex;
        public int MKinectIndex
        {
            get { return mKinectIndex; }
            set
            {
                mKinectIndex = value;
                OnPropertyChanged("MKinectIndex");
            }
        }

        public KinectConfigurationViewModel(Map m)
        {
            Text1 = variables.Variables.KinectConfigurationText;

            MyMap = m;
            MyMap.kinectSensorInit();
            //MKinectInit = kInit;
            //MKinectInit.kinectInit();

            ShowKinectCommand = new DelegateCommand(ShowKinectAction, ShowKinectCanExecute);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowKinectAction(object obj)
        {
            MyMap.moveSensor(MKinectIndex);
            //MKinectInit.moveSensor(MKinectIndex);
        }

        private bool ShowKinectCanExecute(object obj)
        {
            return true;
        }
    }
}
