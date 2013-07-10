using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mvvm.ViewModel.Commnad;
using System.Windows.Controls;
using mvvm.View;
using mvvm.Model;

namespace mvvm.ViewModel
{
    public class StartWindowViewModel : INotifyPropertyChanged
    {
        public Map MyMap { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand Mode1Command { get; set; }
        public DelegateCommand Mode2Command { get; set; }

        public string Text { set; get; }
        public string Text2 { get; set; }
        public StartWindowViewModel(Map m)
        {
            MyMap = m;

            Mode1Command = new DelegateCommand(Mode1Action, Mode1CanExecute);
            Mode2Command = new DelegateCommand(Mode2Action, Mode2CanExecute);
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Mode1Action(object obj)
        {
            Mode1ViewModel vm = new Mode1ViewModel(MyMap);
            if (vm.Flag == 1)
            {
                Mode1Window nw = new Mode1Window(vm);
                nw.ShowDialog();
            }
        }

        private bool Mode1CanExecute(object obj)
        {
            return true;
        }

        private void Mode2Action(object obj)
        {
            Mode2ViewModel vm = new Mode2ViewModel(MyMap);
            if (vm.Flag == 1)
            {
                Mode2Window nw = new Mode2Window(vm);
                nw.ShowDialog();
            }
        }

        private bool Mode2CanExecute(object obj)
        {
            return true;
        }
    }
}
