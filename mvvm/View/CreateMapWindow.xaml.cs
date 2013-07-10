﻿using System;
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
using mvvm.ViewModel;

namespace mvvm.View
{
    /// <summary>
    /// Interaction logic for CreateMapWindow.xaml
    /// </summary>
    public partial class CreateMapWindow : Window
    {
        private CreateMapViewModel viewModel;

        public CreateMapWindow(CreateMapViewModel vm)
        {
            InitializeComponent();

            viewModel = vm;
            DataContext = viewModel;
        }
    }
}
