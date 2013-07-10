using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mvvm.ViewModel.Commnad;
using mvvm.View;
using mvvm.Model;
using Microsoft.Win32;

namespace mvvm.ViewModel
{
    public class CreateMapViewModel : INotifyPropertyChanged
    {
        public Map MyMap { get; set; }

        public DelegateCommand SpaceConfigurationCommand { get; set; }
        public DelegateCommand ConstructionConfigurationCommand { get; set; }
        public DelegateCommand SaveMapCommand { get; set; }
        public DelegateCommand LoadMapCommand { get; set; }


        public CreateMapViewModel(Map m)
        {
            SpaceConfigurationCommand = new DelegateCommand(SpaceConfigurationAction, SpaceConfigurationCanExecute);
            ConstructionConfigurationCommand = new DelegateCommand(ConstructionConfigurationAction, ConstructionConfigurationCanExecute);
            SaveMapCommand = new DelegateCommand(SaveMapAction, SaveMapCanExecute);
            LoadMapCommand = new DelegateCommand(LoadMapAction, LoadMapCanExecute);

            MyMap = m;

            //MKinectInit = kInit;
            //MKinectInit.setMap(MyMap);

////////////////////////////////////
//sztuczne dodanie 2 kinectow
            //MyMap.KinectList.Add(new MKinect("1", "1"));
            //MyMap.KinectList.Add(new MKinect("2", "2"));

            //MyMap.ConstructionList.Add(new MConstruction("a", 100, 100));
            //MyMap.ConstructionList.Add(new MConstruction("b", 200, 200));
            //MyMap.ConstructionList.Add(new MConstruction("C", 0, 0));
////////////////////////////////////
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// uruchamia okno w ktorym ustawimy pozycje kinectow oraz wymiar przestrzeni
        /// </summary>
        /// <param name="obj"></param>
        private void SpaceConfigurationAction(object obj)
        {
            SpaceConfigurationViewModel vm = new SpaceConfigurationViewModel(MyMap);
            SpaceConfigurationWindow nw = new SpaceConfigurationWindow(vm);
            nw.ShowDialog();
        }

        private bool SpaceConfigurationCanExecute(object obj)
        {
            return true;
        }

        /// <summary>
        /// okno ustawianie przeszkod w przestrzeni
        /// </summary>
        /// <param name="obj"></param>
        private void ConstructionConfigurationAction(object obj)
        {
            ConstructionConfigurationViewModel vm = new ConstructionConfigurationViewModel(MyMap);
            ConstructionConfigurationWindow nw = new ConstructionConfigurationWindow(vm);
            nw.ShowDialog();
        }

        private bool ConstructionConfigurationCanExecute(object obj)
        {
            return true;
        }

        /// <summary>
        /// save map to the file
        /// </summary>
        /// <param name="obj"></param>
        private void SaveMapAction(object obj)
        {
            //Map.save(MyMap);
            Serializer ser = new Serializer();
            //ser.SerializeObject("ala", MyMap);
            ///okno zapisywania
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = MyMap.MapName;

            Nullable<bool> result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                MyMap.MapName = saveFileDialog.FileName;
                ser.SerializeObject(saveFileDialog.FileName, MyMap);
            }
            
        }

        private bool SaveMapCanExecute(object obj)
        {
            return true;
        }


        private void LoadMapAction(object obj)
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
                    MyMap.mapCopyFrom(tmp);
                }
            }
        }

        private bool LoadMapCanExecute(object obj)
        {
            return true;
        }

    }
}
