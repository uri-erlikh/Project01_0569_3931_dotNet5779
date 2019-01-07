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
            test.TestHour= DateTime.Now;

            for(int i = 9; i < 15; ++i)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content ="hour: "+ i;
                comboBoxhour.Items.Add(comboBoxItem);                
            }
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTest(test);
                test = new BO.Test();
                this.DataContext = test;
            }
            catch (KeyNotFoundException a)
            {
                MessageBox.Show(a.Message);
            }

        }

        private void ComboBoxhour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            test.TestHour = new DateTime(test.TestDate.Year , test.TestDate.Month , test.TestDate.Day, (comboBoxhour.SelectedIndex+9), 0,0);
        }



        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // testViewSource.Source = [generic data source]
        //}
    }
}
