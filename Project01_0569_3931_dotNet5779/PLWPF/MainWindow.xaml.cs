﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;

namespace PLWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        //-------------------------------------------------------------------
        private void TestersButoon_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you the admin??", "d.m.v.", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Window testersWindow = new TestersWindow("admin");
                testersWindow.Show();
                this.Close();
            }
            else if (result == MessageBoxResult.No)
            {
                new TestersWindow("tester").Show();
                this.Close();
            }
        }
        //---------------------------------------------------------------------
        private void TrainiesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you the admin??", "d.m.v.", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Window trainiesWindow = new TrainiesWindow("admin");
                trainiesWindow.Show();
                this.Close();
            }
            else if (result == MessageBoxResult.No)
            {
                new TrainiesWindow("trainee").Show();
                this.Close();
            }
        }
        //-------------------------------------------------------------------------
        private void TestsButton_Click(object sender, RoutedEventArgs e)
        {
            new TestsWindow().Show();
            this.Close();                        
        }
        //------------------------------------------------------------------------
        private void DataButton_Click(object sender, RoutedEventArgs e)
        {
            new DataWindow().Show();
            this.Close();
        }
        //------------------------------------------------------------------------
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
