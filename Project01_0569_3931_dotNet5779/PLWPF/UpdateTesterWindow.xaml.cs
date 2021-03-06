﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateTesterWindow.xaml
    /// </summary>
    public partial class UpdateTesterWindow : Window
    {
        BL.IBL bl;
        BO.Tester tester = new BO.Tester();

        public UpdateTesterWindow(BO.Tester tester1)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bl = BL.BL_Factory.GetBL();
            tester = tester1;
            numTesterTextBlock.Text = "ID: " + tester.ID;
            this.DataContext = tester;
            foreach (var item in ScheduleGrid.Children.OfType<CheckBox>().ToList())
            {
                int row = Grid.GetRow(item) - 1;
                int column = Grid.GetColumn(item) - 1;
                item.IsChecked = tester.Schedule[row, column];
            }
        }
        //------------------------------------------------------------------------
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                    if (familyNameTextBox.Text == "" || privateNameTextBox.Text == "" || cityTextBox.Text == "" || streetTextBox.Text == "")
                        MessageBox.Show("please fill all fields", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    else
                    {
                        foreach (var item in ScheduleGrid.Children.OfType<CheckBox>().ToList())
                        {
                            int row = Grid.GetRow(item) - 1;
                            int column = Grid.GetColumn(item) - 1;
                            tester.Schedule[row, column] = (bool)item.IsChecked;
                        }
                        bl.UpdateTester(tester);
                        MessageBox.Show("Update succeeded","d.m.v.",MessageBoxButton.OK,MessageBoxImage.Information);
                        new TestersWindow("admin").Show();
                        this.Close();
                    }                
            }
            catch (BO.InvalidDataException r)
            {
                MessageBox.Show(r.Message);
            }
            catch (KeyNotFoundException x)
            {
                MessageBox.Show(x.Message);
            }
        }
        //---------------------------------------------------------------------------
        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void rangeToTestTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void testerExperienceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }
        //---------------------------------------------------------------------------------
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TestersWindow("admin").Show();
            this.Close();
        }        
    }
}
