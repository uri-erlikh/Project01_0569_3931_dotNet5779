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
        string traineeID;
        BO.Vehicle vehicle;

        public TrainiesWindow(string identifier)
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
            this.DataContext = trainee;
            this.GetVehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            if (identifier == "admin")
            {
                this.AddTraineeButton.Visibility = Visibility.Visible;
                this.DeleteTraineeButton.Visibility = Visibility.Visible;
                this.UpdateTraineeButton.Visibility = Visibility.Visible;
            }
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetIDTextBox.Text.Length < 9)
                MessageBox.Show("please insert valid ID - 9 digits", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (int.TryParse(this.GetIDTextBox.Text, out int number) != true)
            {
                MessageBox.Show("please insert only digits for ID", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
                this.GetIDTextBox.Clear();
            }
            else
            {
                try
                {
                    vehicle = (BO.Vehicle)GetVehicleTypeComboBox.SelectedIndex;
                    traineeID = GetIDTextBox.Text;
                    trainee = bl.GetOneTrainee(traineeID, vehicle);
                    this.PrintTraineeButton.IsEnabled = true;
                    this.GetTestOfTTraineeButton.IsEnabled = true;
                    if (this.AddTraineeButton.Visibility == Visibility.Visible)
                    {
                        this.UpdateTraineeButton.IsEnabled = true;
                        this.DeleteTraineeButton.IsEnabled = true;
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
        }
        //--------------------------------------------------------------------------
        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            new AddTraineeWindow().Show();
            this.Close();
        }
        //---------------------------------------------------------------------------
        private void UpdateTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdateTraineeWindow(trainee).Show();
            this.Close();
        }
        //-------------------------------------------------------------------------
        private void DeleteTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Are you sure you want to delete the trainee?", "d.m.v.", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bl.DeleteTrainee(traineeID, vehicle);
                MessageBox.Show("Trainee was deleted from list", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Information);
                reset();
            }
        }
        //------------------------------------------------------------------------------
        private void PrintTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            DataTextBlock.Visibility = Visibility;
            DataTextBlock.Background = Brushes.DarkSeaGreen;
            DataTextBlock.Text = trainee.ToString();
        }
        //-------------------------------------------------------------------
        private void GetTestOfTTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTextBlock.Visibility = Visibility;
                DataTextBlock.Background = Brushes.Gold;
                DataTextBlock.Text = "";
                List<BO.TraineeTest> list = bl.GetFutureTestForTrainee(traineeID, vehicle);
                if (!list.Any())
                    DataTextBlock.Text = "no tests were found";
                else
                    foreach (var item in list)
                    {
                        DataTextBlock.Text += item.ToString() + "\n";
                    }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //----------------------------------------------------------------------
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        //-------------------------------------------------------------
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void reset()
        {
            this.DataTextBlock.Visibility = Visibility.Hidden;
            this.GetIDTextBox.Clear();
            this.GetVehicleTypeComboBox.SelectedIndex = 0;
            this.PrintTraineeButton.IsEnabled = false;
            this.GetTestOfTTraineeButton.IsEnabled = false;
            this.DeleteTraineeButton.IsEnabled = false;
            this.UpdateTraineeButton.IsEnabled = false;
        }
    }
}
