using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mvvm.Model;
using mvvm.ViewModel.Commnad;
using System.Collections.ObjectModel;

namespace mvvm.ViewModel
{
    public class ConstructionConfigurationViewModel : INotifyPropertyChanged
    {
        private double backgroundWidth;
        public double BackgroundWidth 
        {
            get { return backgroundWidth; }
            set
            {
                backgroundWidth = value;
                OnPropertyChanged("BackgroundWidth");
            }
        }

        private double backgroundHeight;
        public double BackgroundHeight
        {
            get { return backgroundHeight; }
            set
            {
                backgroundHeight = value;
                OnPropertyChanged("BackgroundHeight");
            }
        }


        public DelegateCommand AddConstructionCommand { get; set; }
        public DelegateCommand ChangeConstructionNameCommand { get; set; }
        public DelegateCommand ChangeConstructionSoundCommand { get; set; }
        public DelegateCommand DeleteConstructionCommand { get; set; }

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
        /// indeks aktualnie wybranej konstrukcji
        /// </summary>
        private int constructionIndex;
        public int ConstructionIndex
        {
            get { return constructionIndex; }
            set
            {
                constructionIndex = value;
                if (value >= 0)
                {
                    SelectedConstruction = MyMap.ConstructionList[value];
                }
                OnPropertyChanged("StreamName");
                OnPropertyChanged("ConstructionIndex");
            }
        }
        /// <summary>
        /// zaznaczona konstrukcja w celu podswietlenia
        /// </summary>
        private MConstruction selectedConstruction;
        public MConstruction SelectedConstruction
        {
            get { return selectedConstruction; }
            set
            {
                selectedConstruction = value;
                OnPropertyChanged("SelectedConstruction");
            }
        }

        /// <summary>
        /// konstruktor ktory otrzymuje mape
        /// </summary>
        /// <param name="m"></param>
        public ConstructionConfigurationViewModel(Map m)
        {
            MyMap = m;
            //MyMap.kinectSensorInit();
            //MConstructionList = new ObservableCollection<MConstruction>(MyMap.ConstructionList);

            //MyMap.initConfigurationMode();
            KinectInit.MyMap = m;
            KinectInit.initConfigurationMode();
            //poczatkowa pozycja zaznaczonego po za ekran
            SelectedConstruction = new MConstruction("", -100, -100);
            ConstructionIndex = -1;

            AddConstructionCommand = new DelegateCommand(AddConstructionAction);
            ChangeConstructionNameCommand = new DelegateCommand(ChangeConstructionNameAction);
            ChangeConstructionSoundCommand = new DelegateCommand(ChangeConstructionSoundAction);
            DeleteConstructionCommand = new DelegateCommand(DeleteConstructionAction);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// dodanie nowego elementu
        /// </summary>
        /// <param name="obj"></param>
        private void AddConstructionAction(object obj)
        {
            //MyMap.setUserPosition(0, -20, 20);
            //KinectInit.CalculateUserPosition();
            MyMap.placeNewConstruction();
        }

        /// <summary>
        /// zmiana nazwy wybranego elementu
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeConstructionNameAction(object obj)
        {
            if (ConstructionIndex >= 0)
            {
                string s = (string)obj;
                MyMap.ConstructionList[ConstructionIndex].Name = s;
                //MConstructionList[ConstructionIndex].Name = s;
                int tmp = ConstructionIndex;
                ConstructionIndex = -1;
                ConstructionIndex = tmp;
            }

        }


        /// <summary>
        /// zmiana dzwieku dla wybranego elementu
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeConstructionSoundAction(object obj)
        {
            if (ConstructionIndex >= 0)
            {
                string s = (string)obj;
                MyMap.ConstructionList[ConstructionIndex].StreamName = s;
                //MyMap.ConstructionList[ConstructionIndex].StreamName = new string(s.ToCharArray());
                int tmp = ConstructionIndex;
                ConstructionIndex = -1;
                ConstructionIndex = tmp;
            }
        }

        private void DeleteConstructionAction(object obj)
        {
            if (MyMap.ConstructionList.Count > 0 && ConstructionIndex>=0)
            {
                MyMap.removeConstruction(ConstructionIndex);
                if(MyMap.ConstructionList.Count == 0)
                {
                    SelectedConstruction = new MConstruction("", -100, -100);
                }
                else
                {
                    SelectedConstruction = MyMap.ConstructionList[0];
                    ConstructionIndex = 0;
                }
            }
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            // Handle closing logic, set e.Cancel as needed
            KinectInit.initConfigurationModeStop();
        }

        
    }
}
