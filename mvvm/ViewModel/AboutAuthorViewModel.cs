using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace mvvm.ViewModel
{
    class AboutAuthorViewModel : INotifyPropertyChanged
    {
        public string AboutAuthor { get; set; }
        public AboutAuthorViewModel()
        {
            AboutAuthor = variables.Variables.AboutAuthorText;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
