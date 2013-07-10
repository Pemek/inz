using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NAudio.Wave;

namespace mvvm.Model
{
    [Serializable]
    public class MConstruction : INotifyPropertyChanged
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string streamName;
        public string StreamName
        {
            get { return streamName; }
            set
            {
                //streamName = "..\\..\\sounds\\" + value + ".mp3";
                streamName = value;
                OnPropertyChanged("StreamName");
            }
        }
        

        public MConstruction(string name, double x, double y)
        {
            X = x;
            Y = y;
            Name = name;
            StreamName = "";
        }

        #region
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
