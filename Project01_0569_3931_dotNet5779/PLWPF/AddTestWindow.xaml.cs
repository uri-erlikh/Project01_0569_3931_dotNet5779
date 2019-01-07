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
    public partial class AddTestWindow : Window
    {
        BO.Test test = new BO.Test();
        BL.IBL bl;

        public AddTestWindow()
        {
            InitializeComponent();
            
            this.DataContext = test;
            
            bl = BL.BL_Factory.GetBL();

            this.vehicleComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            test.TestDate = DateTime.Now;
            for (int i=9; i <= 14; ++i)
            {
                ComboBoxItem comboItem = new ComboBoxItem();
                comboItem.Content = i;
                testHourComboBox.Items.Add(comboItem);                
            }
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTest(test);
                test = new BO.Test();
                this.DataContext = test;
                MessageBox.Show("successfully completed");
            }
            catch (KeyNotFoundException a)
            {
                MessageBox.Show(a.Message, "exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.InvalidDataException a)
            {
                MessageBox.Show(a.Message, "exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void testHourComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            test.TestHour = new DateTime(test.TestDate.Year, test.TestDate.Month, test.TestDate.Day, (int)testHourComboBox.SelectedItem, 0, 0);

        }
    }
}
