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
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    /// 
    
    public partial class AddTraineeWindow : Window
    {
        BO.Trainee myTrainee = new BO.Trainee();
        BL.IBL bl;

        public AddTraineeWindow()
        {
            InitializeComponent();

            myTrainee.DayOfBirth = new DateTime(1999,01,01);
            this.DataContext = myTrainee;
            bl = BL.BL_Factory.GetBL();
            this.traineeVehicleComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            this.traineeGearComboBox.ItemsSource = Enum.GetValues(typeof(BO.GearBox));
            this.personGenderComboBox.ItemsSource = Enum.GetValues(typeof(BO.Gender));
        }
        //-------------------------------------------------------------------
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IDTextBox.Text.Length < 9)
                    MessageBox.Show("please insert valid ID - 9 digits", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (int.TryParse(this.IDTextBox.Text, out int number) != true ||
                    int.TryParse(this.phoneTextBox.Text, out int number1) != true||
                        int.TryParse(this.numOfBuildingTextBox.Text, out int number2) != true)
                    MessageBox.Show("please insert only digits for: ID, phone and num of building", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);                
                else
                {
                    if (this.traineeGearComboBox.SelectedItem == null || this.traineeVehicleComboBox.Text == null||
                       this.personGenderComboBox.SelectedItem==null || this.privateNameTextBox.Text == "" || this.familyNameTextBox.Text == ""
                       || this.phoneTextBox.Text == "" || this.schoolTextBox.Text == "" || this.teacherTextBox.Text == "" || this.cityTextBox.Text == ""
                       || this.streetTextBox.Text == "" || this.numOfBuildingTextBox.Text == "" || this.drivingLessonsNumTextBox.Text == "")
                        MessageBox.Show("please fill all fields", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    else
                    {
                        myTrainee.TraineeVehicle = (BO.Vehicle)this.traineeVehicleComboBox.SelectedIndex;
                        myTrainee.PersonGender = (BO.Gender)this.personGenderComboBox.SelectedIndex;
                        myTrainee.TraineeGear = (BO.GearBox)this.traineeGearComboBox.SelectedIndex;
                        bl.AddTrainee(myTrainee);
                        MessageBox.Show("Welcome to our system. Good Luck!!", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        myTrainee = new BO.Trainee();
                        myTrainee.DayOfBirth = new DateTime(1999, 01, 01);
                        this.IDTextBox.Clear();
                        this.DataContext = myTrainee;
                        this.traineeGearComboBox.SelectedItem = null;
                        this.traineeVehicleComboBox.SelectedItem = null;
                        this.personGenderComboBox.SelectedItem = null;
                        this.streetTextBox.Clear();
                    }
                }
            }
            catch (DuplicateWaitObjectException a)
            {
                MessageBox.Show(a.Message);
            }
            catch (BO.InvalidDataException a)
            {
                MessageBox.Show(a.Message);
            }
        }
        //--------------------------------------------------------------------
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.traineeGearComboBox.SelectedItem = null;
            this.traineeVehicleComboBox.SelectedItem = null;
            this.personGenderComboBox.SelectedItem = null;
            this.IDTextBox.Clear();
            this.privateNameTextBox.Clear();
            this.familyNameTextBox.Clear();
            this.phoneTextBox.Clear();
            this.schoolTextBox.Clear();
            this.teacherTextBox.Clear();
            this.cityTextBox.Clear();
            this.drivingLessonsNumTextBox.Clear();
            this.numOfBuildingTextBox.Clear();
            this.streetTextBox.Clear();
        }
        //----------------------------------------------------------------------
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

        private void drivingLessonsNumTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("Insert numbers only!", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }
            //------------------------------------------------------------------
            private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TrainiesWindow("admin").Show();
            this.Close();
        }        
        
    }
}
