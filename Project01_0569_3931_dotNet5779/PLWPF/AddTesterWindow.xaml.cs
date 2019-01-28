using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddTesterWindow.xaml//
    /// </summary>
    public partial class AddTesterWindow : Window
    {
        
        BO.Tester tester = new BO.Tester();
        BL.IBL bl;

        public AddTesterWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
            tester.DayOfBirth = new DateTime(1969, 01, 01);
            this.DataContext = tester;
            bl = BL.BL_Factory.GetBL();
            this.testerVehicleComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            this.personGenderComboBox.ItemsSource = Enum.GetValues(typeof(BO.Gender));
        }
        //------------------------------------------------------------------
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IDTextBox.Text.Length < 9)
                    MessageBox.Show("please insert valid ID - 9 digits", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (this.testerVehicleComboBox.Text == null
                        || this.personGenderComboBox.SelectedItem == null || this.privateNameTextBox.Text == "" || this.familyNameTextBox.Text == ""
                       || this.phoneTextBox.Text == "" || this.cityTextBox.Text == ""|| this.streetTextBox.Text == "" || this.numOfBuildingTextBox.Text == "" 
                       ||this.rangeToTestTextBox.Text=="" || this.testerExperienceTextBox.Text=="" ||this.maxWeeklyTestsTextBox.Text=="")
                        MessageBox.Show("please fill all fields", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    else
                    {
                        foreach (var item in ScheduleGrid.Children.OfType<CheckBox>().ToList())
                        {
                            int row = Grid.GetRow(item)-1;
                            int column = Grid.GetColumn(item)-1;
                            tester.Schedule[row, column] = (bool)item.IsChecked;
                        }

                        tester.TesterVehicle = (BO.Vehicle)this.testerVehicleComboBox.SelectedIndex;
                        tester.PersonGender = (BO.Gender)this.personGenderComboBox.SelectedIndex;
                        bl.AddTester(tester,tester.Schedule);
                        MessageBox.Show("Welcome to our system. Good Luck!!", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        tester = new BO.Tester();
                        tester.DayOfBirth = new DateTime(1969, 01, 01);
                        this.IDTextBox.Clear();
                        this.DataContext = tester;
                        this.personGenderComboBox.SelectedItem = null;
                        this.testerVehicleComboBox.SelectedItem = null;
                    }
                }
            }
            catch (DuplicateWaitObjectException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.InvalidDataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-----------------------------------------------------------------
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.personGenderComboBox.SelectedItem = null;
            this.testerVehicleComboBox.SelectedItem = null;
            this.IDTextBox.Clear();
            this.privateNameTextBox.Clear();
            this.familyNameTextBox.Clear();
            this.phoneTextBox.Clear();
            this.cityTextBox.Clear();
            this.numOfBuildingTextBox.Clear();
            this.streetTextBox.Clear();
            this.testerExperienceTextBox.Clear();
            this.rangeToTestTextBox.Clear();
            this.maxWeeklyTestsTextBox.Clear();
            // this.dayOfBirthDatePicker.
        }
        //--------------------------------------------------------------------------
        private void IDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void numOfBuildingTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void rangeToTestTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void testerExperienceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void maxWeeklyTestsTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }
        //-----------------------------------------------------------------------
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TestersWindow("admin").Show();
            this.Close();
        }

        

    }       
 }
