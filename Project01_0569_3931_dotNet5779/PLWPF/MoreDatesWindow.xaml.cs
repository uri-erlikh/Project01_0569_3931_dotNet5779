﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PLWPF
{

    /// <summary>
    /// Interaction logic for MoreDatesWindow.xaml
    /// </summary>
    /// 
    public class Dates
    {
        public string DayTime { get; set; }
        public string HourTime { get; set; }
        public List<DateTime> Times = new List<DateTime>();
        DateTime startDate = new DateTime();
        public DateTime StartDate { get; set; }
        DateTime endtDate = new DateTime();
        public DateTime EndDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int NumOfBilding { get; set; }
        public BO.Vehicle vehicle { get; set; }
        //public override List<string> ToString()
        //{
        //        foreach (var item in Times)
        //            date1.Add( item.Day + "." + item.Month + "." + item.Year + " at: " + item.Hour + ":" + "00  ");
        //    return date1;
        //}

    }
    //---------------------------------------
    public partial class MoreDatesWindow : Window
    {
        List<Dates> dates1 = new List<Dates>();
        BL.IBL bl;
        Dates dates = new Dates();
        public MoreDatesWindow()
        {
            InitializeComponent();
            DataContext = dates;
            bl = BL.BL_Factory.GetBL();
            dates.EndDate = DateTime.Now.AddDays(1);
            dates.StartDate= DateTime.Now.AddDays(2);
            this.GetVehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cityTextBox.Text == "" || streetTextBox.Text == "" || GetVehicleTypeComboBox.ItemsSource == null)
                    MessageBox.Show("please fill all fields", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                else
                {
                    if (int.TryParse(numOfBuildingTextBox.Text, out int number) != true)
                        MessageBox.Show("please insert only digits for Number of bilding", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        dates.vehicle = (BO.Vehicle)GetVehicleTypeComboBox.SelectedIndex;
                        dates.Times = bl.GetDateOfTests(dates.StartDate, dates.EndDate, dates.City, dates.Street, dates.NumOfBilding, dates.vehicle);
                        foreach(var item in dates.Times)
                        {
                            dates.DayTime = item.Day + "." + item.Month + "." + item.Year;
                            dates.HourTime =  item.Hour + ":" + "00  ";
                            dates1.Add(new Dates() { DayTime = dates.DayTime, HourTime = dates.HourTime });
                        }
                        
                        DatesListView.ItemsSource = dates1;
                    }
                }
            }
            catch (KeyNotFoundException a)
            {
                MessageBox.Show(a.Message);                
            }
            catch (BO.InvalidDataException a)
            {
                MessageBox.Show(a.Message);               
            }
            catch(IndexOutOfRangeException a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void BackButtom_Click(object sender, RoutedEventArgs e)
        {
            new AddTestWindow().Show();
            this.Close();
        }

       
    }
}
