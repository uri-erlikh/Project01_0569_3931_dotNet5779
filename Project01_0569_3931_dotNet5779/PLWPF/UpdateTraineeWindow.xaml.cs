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
    /// Interaction logic for UpdateTraineeWindow.xaml
    /// </summary>
    /// 

    public partial class UpdateTraineeWindow : Window
    {
        BL.IBL bl;
        BO.Trainee trainee = new BO.Trainee();
        public UpdateTraineeWindow(BO.Trainee trainee1)
        {
            InitializeComponent();
            numTraineeTextBlock.Text = "ID: " + trainee1.ID;
            bl = BL.BL_Factory.GetBL();
            trainee = trainee1;
            traineeGearComboBox.ItemsSource = Enum.GetValues(typeof(BO.GearBox));
            this.DataContext = trainee;
            this.traineeGearComboBox.SelectedValue = trainee.TraineeGear;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.TraineeGear = (BO.GearBox)this.traineeGearComboBox.SelectedIndex;
                if (familyNameTextBox.Text == "" || privateNameTextBox.Text == "" || schoolTextBox.Text == "" ||
                    traineeGearComboBox.ItemsSource == null || cityTextBox.Text == "" || streetTextBox.Text == "")
                    MessageBox.Show("please fill all fields", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                else
                {
                    bl.UpdateTrainee(trainee);
                    MessageBox.Show("Update is succeeded");
                    new TrainiesWindow("admin").Show();
                    this.Close();
                }
            }
            catch (BO.InvalidDataException r)
            {
                MessageBox.Show(r.Message,"d.m.v",MessageBoxButton.OK,MessageBoxImage.Stop);
            }
            catch (KeyNotFoundException x)
            {
                MessageBox.Show(x.Message);
            }
        }
        //------------------------------------------------------------------------
        private void drivingLessonsNumTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        //-----------------------------------------------------------------------------
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TrainiesWindow("admin").Show();
            this.Close();
        }
    }
}
