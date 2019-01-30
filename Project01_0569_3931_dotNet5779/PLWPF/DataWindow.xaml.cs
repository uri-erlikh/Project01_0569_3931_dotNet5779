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
using System.Text.RegularExpressions;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {       
        BL.IBL bl;

        ObservableCollection<BO.Tester> testers;
        ObservableCollection<BO.Trainee> trainees;

        IEnumerable<IGrouping<BO.Vehicle, BO.Tester>> testerByVehicle;
        IEnumerable<IGrouping<string, BO.Trainee>> traineesBySchool;
        IEnumerable<IGrouping<string, BO.Trainee>> traineesByTeacher;
        IEnumerable<IGrouping<int, BO.Trainee>> traineesByTestNum;

        List<string> configValues = new List<string>() { "MIN_LESSONS", "MAX_TESTER_AGE", "MIN_TRAINEE_AGE", "MIN_GAP_TEST", "MIN_TESTER_AGE" };
        

        public DataWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bl = BL.BL_Factory.GetBL();
            this.configComboBox.ItemsSource = configValues;
        }
        //----------------------------------------------------------------------------        
        private void testersPerVehicle_Selected(object sender, RoutedEventArgs e)
        {
            reset();
            vehicleComboBox.IsEnabled = true;
            schoolComboBox.IsEnabled = false;
            teacherComboBox.IsEnabled = false;
            NumberOfTestsComboBox.IsEnabled = false;

            MessageBoxResult result = MessageBox.Show("to show ordered by family names??", "d.m.v.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                testerByVehicle = bl.TestersByVehicle(true);
            else
                testerByVehicle = bl.TestersByVehicle(false);
            List<BO.Vehicle> myKey = (from item in testerByVehicle select item.Key).ToList();
            vehicleComboBox.ItemsSource = myKey;
        }

        private void vehicleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in testerByVehicle)
            {
                if (vehicleComboBox.SelectedItem != null)
                {
                    if (item.Key == (BO.Vehicle)vehicleComboBox.SelectedItem)
                    {
                        testers = new ObservableCollection<BO.Tester>(item);
                        this.DetailsTesterListView.ItemsSource = testers;
                    }
                }
            }
        }
        //----------------------------------------------------------------------------
        private void traineesPerSchool_Selected(object sender, RoutedEventArgs e)  
        {
            reset();
            schoolComboBox.IsEnabled = true;
            vehicleComboBox.IsEnabled = false;
            teacherComboBox.IsEnabled = false;
            NumberOfTestsComboBox.IsEnabled = false;

            MessageBoxResult result = MessageBox.Show("to show ordered by family names??", "d.m.v.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                traineesBySchool = bl.TraineesBySchool(true);
            else
                traineesBySchool = bl.TraineesBySchool(false);
            List<string> myKey = (from item in traineesBySchool select item.Key).ToList();
            schoolComboBox.ItemsSource = myKey;
        }

        private void schoolComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in traineesBySchool)
            {
                if (schoolComboBox.SelectedItem != null)
                {
                    if (item.Key == (string)schoolComboBox.SelectedItem)
                    {
                        trainees = new ObservableCollection<BO.Trainee>(item);
                        DetailsTraineeListView.ItemsSource = trainees;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------
        private void traineesPerTeacher_Selected(object sender, RoutedEventArgs e)
        {
            reset();
            schoolComboBox.IsEnabled = false;
            vehicleComboBox.IsEnabled = false;
            teacherComboBox.IsEnabled = true;
            NumberOfTestsComboBox.IsEnabled = false;

            MessageBoxResult result = MessageBox.Show("to show ordered by family names??", "d.m.v.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                traineesByTeacher = bl.TraineesByTeacher(true);
            else
                traineesByTeacher = bl.TraineesByTeacher(false);
            List<string> myKey = (from item in traineesByTeacher select item.Key).ToList();
            teacherComboBox.ItemsSource = myKey;
        }

        private void teacherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in traineesByTeacher)
            {
                if (teacherComboBox.SelectedItem != null)
                {
                    if (item.Key == (string)teacherComboBox.SelectedItem)
                    {
                        trainees = new ObservableCollection<BO.Trainee>(item);
                        DetailsTraineeListView.ItemsSource = trainees;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------
        private void traineesPerTests_Selected(object sender, RoutedEventArgs e)
        {
            reset();
            schoolComboBox.IsEnabled = false;
            vehicleComboBox.IsEnabled = false;
            teacherComboBox.IsEnabled = false;
            NumberOfTestsComboBox.IsEnabled = true;

            MessageBoxResult result = MessageBox.Show("to show ordered by family names??", "d.m.v.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                traineesByTestNum = bl.TraineesByTests(true);
            else
                traineesByTestNum = bl.TraineesByTests(false);
            List<int> myKey = (from item in traineesByTestNum select item.Key).ToList();
            NumberOfTestsComboBox.ItemsSource = myKey;
        }

        private void NumberOfTestsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in traineesByTestNum)
            {
                if (NumberOfTestsComboBox.SelectedItem != null)
                {
                    if (item.Key == (int)NumberOfTestsComboBox.SelectedItem)
                    {
                        trainees = new ObservableCollection<BO.Trainee>(item);
                        DetailsTraineeListView.ItemsSource = trainees;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------       
        private void getTesters_Selected(object sender, RoutedEventArgs e)
        {
            reset();
            List<BO.Tester> allTesters = new List<BO.Tester>(bl.GetTesters());
            DetailsTesterListView.ItemsSource = allTesters;
        }

        private void getTraineess_Selected(object sender, RoutedEventArgs e)
        {
            reset();
            List<BO.Trainee> allTrainees = new List<BO.Trainee>(bl.GetTrainees());
            DetailsTraineeListView.ItemsSource = allTrainees;
        }

        private void getTests_Selected(object sender, RoutedEventArgs e)
        {
            reset();
            List<BO.Test> allTests = new List<BO.Test>(bl.GetTests());
            DetailsTestListView.ItemsSource = allTests;
        }
        //------------------------------------------------------------------------       
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VehicleTraineeComboBox.SelectedItem = null;
                VehicleTraineeComboBox.IsEnabled = true;
                VehicleTraineeComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            }
        }

        private void VehicleTraineeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (bl.IfPassed(IDtextBox.Text, (BO.Vehicle)VehicleTraineeComboBox.SelectedIndex) == true)
                    InfoTextBlock.Text = "registered for " + bl.NumOfTest(IDtextBox.Text, (BO.Vehicle)VehicleTraineeComboBox.SelectedIndex)
                        + " tests and passed!";
                else
                    InfoTextBlock.Text = "registered for " + bl.NumOfTest(IDtextBox.Text, (BO.Vehicle)VehicleTraineeComboBox.SelectedIndex)
                        + " tests and not passed yet!";
            }
            catch (KeyNotFoundException ex)
            {
                InfoTextBlock.Text = ex.Message;
            }
        }
        //----------------------------------------------------------------
        private void configComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            configValuesTextBox.IsEnabled = true;
            configValuesTextBox.Text = bl.GetConfig()[configComboBox.SelectedItem.ToString()].ToString();
        }

        private void configValuesTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void configValuesTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter && configValuesTextBox.Text != null)
                {
                    bl.SetConfig(configComboBox.SelectedItem.ToString(), configValuesTextBox.Text);
                    MessageBox.Show("Done!", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //----------------------------------------------------       
        private void reset()
        {
            NumberOfTestsComboBox.SelectedItem = null;
            teacherComboBox.SelectedItem = null;
            schoolComboBox.SelectedItem = null;
            vehicleComboBox.SelectedItem = null;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

       
    }
}
