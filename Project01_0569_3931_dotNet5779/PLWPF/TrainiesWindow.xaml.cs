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
    /// Interaction logic for Trainies.xaml
    /// </summary>
    public partial class TrainiesWindow : Window
    {
        BO.Trainee trainee = new BO.Trainee();
        BL.IBL bl;


        public TrainiesWindow()
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
            this.DataContext = trainee;
            this.GetVehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
        }

        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateTraineeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure you want to delete the tester?");
        }

        private void PrintTraineeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void GetIDButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
