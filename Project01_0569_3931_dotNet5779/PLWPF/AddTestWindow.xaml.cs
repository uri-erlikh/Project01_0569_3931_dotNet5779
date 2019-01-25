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
using BL;

namespace PLWPF
{

    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    /// 
    //public class Dates
    //{
    //    string date;
    //    public List<DateTime> Times = new List<DateTime>();
    //    public DateTime StartDate = new DateTime();
    //    public DateTime EndtDate = new DateTime();
    //    public override string ToString()
    //    {
    //        foreach (var item in Times)
    //            date = item.Day + "." + item.Month + "." + item.Year+ "   ";
    //        return date;
    //    }

    //}
    //------------------------------------------------------
    //public class ConvertNumOfBilding : IValueConverter
    //{
    //    object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        int num = (int)value;
    //        return num;
    //    }
    //    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return (string)value;
    //    }
    //}
    //-----------------------------------------------------
    public partial class AddTestWindow : Window
    {
        BO.Test test = new BO.Test();
        BL.IBL bl;

        public AddTestWindow()
        {
            InitializeComponent();

            this.DataContext = test;
            bl = BL.BL_Factory.GetBL();
            test.TestDate = DateTime.Now;
            test.TestHour = DateTime.Now;

            this.vehicleComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            for (int i = 9; i < 15; ++i)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = "hour: " + i;
                comboBoxhour.Items.Add(comboBoxItem);
            }
        }
        //-----------------------------------------------------------------------
        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.traineeIdTextBox.Text.Length < 9)
                    MessageBox.Show("please insert valid ID - 9 digits", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (int.TryParse(this.traineeIdTextBox.Text, out int number) != true ||
                   int.TryParse(this.numOfBuildingTextBox.Text, out int number1) != true)
                {
                    MessageBox.Show("please insert only digits for ID", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.traineeIdTextBox.Clear();
                }
                else
                {
                    if (this.comboBoxhour.SelectedItem == null || this.cityTextBox.Text == ""
                        || this.streetTextBox.Text == "" || this.numOfBuildingTextBox.Text == "")
                        MessageBox.Show("please fill all fields", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    else
                    {
                       
                       
                        test.TestHour = new DateTime(test.TestDate.Year, test.TestDate.Month, test.TestDate.Day, (comboBoxhour.SelectedIndex + 9), 0, 0);
                        test.Vehicle = (BO.Vehicle)this.vehicleComboBox.SelectedIndex;
                        MessageBox.Show(bl.AddTest(test),"d.m.v.",MessageBoxButton.OK,MessageBoxImage.Information);
                        //MessageBoxResult result;
                        //result = MessageBox.Show("If you want more dates press YES", "d.m.v.", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                        //if (result == MessageBoxResult.Yes)
                        //    MoreDatesButton.IsEnabled = true;
                        test = new BO.Test();
                        this.traineeIdTextBox.Clear();
                        test.TestDate = DateTime.Now;
                        test.TestHour = DateTime.Now;
                        this.DataContext = test;
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
        }
        //---------------------------------------------------------------------
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TestsWindow().Show();
            this.Close();
        }

        private void MoreDatesButton_Click(object sender, RoutedEventArgs e)
        {
            new MoreDatesWindow().Show();
            this.Close();
            
        }
        //  try
        //    {
        //        Dates dates = new Dates();
        //        dates.Times = bl.GetDateOfTests(dates.StartDate, dates.EndtDate, test);
        //        MoreDatesTextBlock.Text = dates.ToString();
        //    }
        //    catch (KeyNotFoundException a)
        //    {
        //        MessageBox.Show(a.Message);

            //    }
            //    catch (BO.InvalidDataException a)
            //    {
            //        MessageBox.Show(a.Message);

            //    }
            //}
        }
}
