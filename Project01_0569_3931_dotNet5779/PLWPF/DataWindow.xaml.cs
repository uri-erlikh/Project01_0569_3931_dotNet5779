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
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        BL.IBL bl;

        public DataWindow()
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
        }
        //----------------------------------------------------------------------------

        IEnumerable<IGrouping<BO.Vehicle, BO.Tester>> tmp;
        private void testersPerVehicle_Selected(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("to show ordered by family names??", "d.m.v.", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                tmp = bl.TestersByVehicle(true);
            List<BO.Vehicle> myKey = (from item in tmp select item.Key).ToList();
            comboBox.ItemsSource = myKey;
            
        }
        //----------------------------------------------------------------------------
        private void traineesPerSchool_Selected(object sender, RoutedEventArgs e)
        {

        }
        //---------------------------------------------------------------------------
        private void traineesPerTeacher_Selected(object sender, RoutedEventArgs e)
        {

        }
        //---------------------------------------------------------------------------
        private void traineesPerTests_Selected(object sender, RoutedEventArgs e)
        {

        }
        //------------------------------------------------------------------------
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
