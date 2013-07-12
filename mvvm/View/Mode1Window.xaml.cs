using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using mvvm.Model;
using mvvm.ViewModel;

namespace mvvm.View
{
    /// <summary>
    /// Interaction logic for Mode1Window.xaml
    /// </summary>
    public partial class Mode1Window : Window
    {
        Mode1ViewModel viewModel;
        public Mode1Window(Mode1ViewModel vm)
        {
            InitializeComponent();


            viewModel = vm;
            DataContext = vm;

            Closing += viewModel.OnWindowClosing;
        }
    }
}
