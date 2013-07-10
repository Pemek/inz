using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mvvm.ViewModel.Commnad;
using System.Windows.Controls;
using mvvm.View;
using System.Windows;
using mvvm.Model;

namespace mvvm.ViewModel
{
    class MainPageViewModel //: INotifyPropertyChanged
    {
        public string WelcomeText { get; set; }
        public string StartText { get; set; }
        public string MapKreator { get; set; }

        public string PassText { get; set; }
        public string AboutAuthor { get; set; }

        public DelegateCommand HelloCommand { get; set; }
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand MapKreatorCommand { get; set; }
        public DelegateCommand AuthorCommand { get; set; }
        public DelegateCommand KincetConfigurationCommand { get; set; }

        public Map MyMap { get; set; }

        public MainPageViewModel()
        {
            WelcomeText = variables.Variables.HelloText;
            StartText = variables.Variables.StartText;
            MapKreator = variables.Variables.MapKreator;
            AboutAuthor = variables.Variables.AboutAuthor;

            HelloCommand = new DelegateCommand(HelloAction, HelloCanExecute);
            StartCommand = new DelegateCommand(StartAction, StartCanExecute);
            MapKreatorCommand = new DelegateCommand(MapAction, MapCanExecute);
            AuthorCommand = new DelegateCommand(AuthorAction, AuthorCanExecute);
            KincetConfigurationCommand = new DelegateCommand(KincetConfigurationAction, KincetConfigurationCanExecute);

            MyMap = new Map("nowa mapa");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        
        
        private void HelloAction(object obj)
        {
            System.Diagnostics.Debug.WriteLine("icommand test! " + obj);
            PassText = (String)obj;
        }

        private bool HelloCanExecute(object obj)
        {
            return true;
        }

        /// <summary>
        /// klikniecie start
        /// pokazanie nowego okna
        /// </summary>
        /// <param name="obj"></param>
        private void StartAction(object obj)
        {
            StartWindowViewModel vm = new StartWindowViewModel(MyMap);
            Window nw =  new StartWindow(vm);
            nw.ShowDialog();
        }
        
        private bool StartCanExecute(object obj)
        {
            return true;
        }
        /// <summary>
        /// klikniecie na przycisk KreatorMap
        /// przenosi nas do nawej strony ktora pozwala na utworzenie nowej mapy
        /// </summary>
        /// <param name="obj"></param>
        private void MapAction(object obj)
        {
            CreateMapViewModel vm = new CreateMapViewModel(MyMap);
            CreateMapWindow nw = new CreateMapWindow(vm);
            nw.ShowDialog();
        }
        private bool MapCanExecute(object obj)
        {
            return true;
        }
        /// <summary>
        /// klikniecie przycisku o Autorze
        /// strona wyswietla informacje na temat autora pracy
        /// </summary>
        /// <param name="obj"></param>
        private void AuthorAction(object obj)
        {
            AboutAuthorWindow nw = new AboutAuthorWindow();
            nw.ShowDialog();
        }
        private bool AuthorCanExecute(object obj)
        {
            return true;
        }

        private void KincetConfigurationAction(object obj)
        {
            KinectConfigurationViewModel vm = new KinectConfigurationViewModel(MyMap);
            KinectConfigurationWindow nw = new KinectConfigurationWindow(vm);
            nw.ShowDialog();
        }

        private bool KincetConfigurationCanExecute(object obj)
        {
            return true;
        }


        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            // Handle closing logic, set e.Cancel as needed
        }

    }
}
