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
using System.Collections.ObjectModel;

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
        // private List<BO.Trainee> trainees = new List<BO.Trainee>();
        ObservableCollection<string> _trainiesID = new ObservableCollection<string>();
        ObservableCollection<BO.Vehicle> _vehicles = new ObservableCollection<BO.Vehicle>();
        private ObservableCollection<BO.Trainee> trainees = new ObservableCollection<BO.Trainee>();
        bool check = true;
        //--------------------------------------------------------------------
        public TrainiesWindow(string identifier)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bl = BL.BL_Factory.GetBL();
            this.DataContext = trainee;
            this.GetVehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            if (identifier == "admin")
            {
                this.AddTraineeButton.Visibility = Visibility.Visible;
                this.DeleteTraineeButton.Visibility = Visibility.Visible;
                this.UpdateTraineeButton.Visibility = Visibility.Visible;
            }
            if (BO.Trainee.traineesRecentlyOpened.Any())
            {
                foreach (var item in BO.Trainee.traineesRecentlyOpened)
                {
                    _trainiesID.Add(item.ID);
                    _vehicles.Add(item.TraineeVehicle);
                    ListBoxItem listBoxItem = new ListBoxItem();
                    listBoxItem.Content = item;
                    DetailsListBox.Items.Add(listBoxItem);
                }
            }
        }
        //-------------------------------------------------------------------
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
                    if (!trainees.Any() || trainees[0].ID != trainee.ID || trainees[0].TraineeVehicle != trainee.TraineeVehicle)
                    {
                        trainees.Clear();
                        trainees.Add(trainee);
                        foreach (var item in BO.Trainee.traineesRecentlyOpened)
                        {
                            if (traineeID == item.ID && vehicle == item.TraineeVehicle)
                                check = false;
                        }
                        if (check)
                        {
                            if (BO.Trainee.traineesRecentlyOpened.Count < 5)
                            {
                                BO.Trainee.traineesRecentlyOpened.Enqueue(trainee);
                                _trainiesID.Add(trainee.ID);
                                _vehicles.Add(trainee.TraineeVehicle);
                                ListBoxItem listBoxItem = new ListBoxItem();
                                listBoxItem.Content = trainee;
                                DetailsListBox.Items.Add(listBoxItem);
                            }
                            else
                            {
                                BO.Trainee.traineesRecentlyOpened.Dequeue();
                                BO.Trainee.traineesRecentlyOpened.Enqueue(trainee);
                                DetailsListBox.Items.Clear();
                                _trainiesID.Clear();
                                _vehicles.Clear();
                                foreach (var item in BO.Trainee.traineesRecentlyOpened)
                                {
                                    _trainiesID.Add(item.ID);
                                    _vehicles.Add(item.TraineeVehicle);
                                    ListBoxItem listBoxItem = new ListBoxItem();
                                    listBoxItem.Content = item;
                                    DetailsListBox.Items.Add(listBoxItem);
                                }
                            }
                        }
                    }
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
                catch (InvalidOperationException a)
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
                try
                {
                    bl.DeleteTrainee(traineeID, vehicle);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(a.Message);
                }
                MessageBox.Show("Trainee was deleted from list", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Information);
                reset();
            }
        }
        //------------------------------------------------------------------------------
        private void PrintTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            // trainees.Clear();
            this.DataTextBlock.Visibility = Visibility.Hidden;
            this.DetailsTestListView.Visibility = Visibility.Hidden;
            this.DetailsTraineeListView.Visibility = Visibility.Visible;

            //rainees.Add(trainee);
            this.DetailsTraineeListView.ItemsSource = trainees;
        }
        //-------------------------------------------------------------------
        private void GetTestOfTTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DetailsTraineeListView.Visibility = Visibility.Hidden;
                this.DataTextBlock.Text = "";
                List<BO.TraineeTest> list = bl.GetFutureTestForTrainee(traineeID, vehicle);
                if (!list.Any())
                {
                    this.DataTextBlock.Visibility = Visibility.Visible;
                    this.DataTextBlock.Background = Brushes.Gold;
                    this.DataTextBlock.Text = "no tests were found";
                }
                else
                {
                    this.DetailsTestListView.Visibility = Visibility.Visible;
                    this.DetailsTestListView.ItemsSource = list;
                }
                //foreach (var item in list)
                //{
                //    DataTextBlock.Text += item.ToString() + "\n";
                //}
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
            this.DetailsTraineeListView.Visibility = Visibility.Hidden;
            this.DetailsTestListView.Visibility = Visibility.Hidden;
            this.DataTextBlock.Visibility = Visibility.Hidden;
            this.GetIDTextBox.Clear();
            trainees.Clear();
            this.GetVehicleTypeComboBox.SelectedItem = null;
            this.PrintTraineeButton.IsEnabled = false;
            this.GetTestOfTTraineeButton.IsEnabled = false;
            this.DeleteTraineeButton.IsEnabled = false;
            this.UpdateTraineeButton.IsEnabled = false;
        }
        //-----------------------------------------------------------------------------
        private void DetailsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reset();
            traineeID = _trainiesID[DetailsListBox.SelectedIndex];
            vehicle = _vehicles[DetailsListBox.SelectedIndex];
            GetIDTextBox.Text = traineeID;
            GetVehicleTypeComboBox.SelectedIndex = (int)vehicle;
            try
            {
                trainee = bl.GetOneTrainee(GetIDTextBox.Text, vehicle);
                trainees.Add(trainee);
                this.PrintTraineeButton.IsEnabled = true;
                this.GetTestOfTTraineeButton.IsEnabled = true;
                if (this.AddTraineeButton.Visibility == Visibility.Visible)
                {
                    this.UpdateTraineeButton.IsEnabled = true;
                    this.DeleteTraineeButton.IsEnabled = true;
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.InvalidDataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException a)
            {
                MessageBox.Show(a.Message);
            }
        }
    }
}
