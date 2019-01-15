using System;
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
        string date;
        public List<DateTime> Times = new List<DateTime>();
        DateTime startDate = new DateTime();
        public DateTime StartDate { get; set; }
        DateTime endtDate = new DateTime();
        public DateTime EndDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int NumOfBilding { get; set; }
        public BO.Vehicle vehicle { get; set; }
        public override string ToString()
        {
                foreach (var item in Times)
                    date += item.Day + "." + item.Month + "." + item.Year + " at: " + item.Hour + ":" + "00  " + "\n";
            return date;
        }

    }
    //---------------------------------------
    public partial class MoreDatesWindow : Window
    {
        BL.IBL bl;
        Dates dates = new Dates();
        public MoreDatesWindow()
        {
            InitializeComponent();
            DataContext = dates;
            bl = BL.BL_Factory.GetBL();
            dates.EndDate = DateTime.Now;
            dates.StartDate= DateTime.Now;
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
                        MoreDatesTextBlock.Text = dates.ToString();
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
