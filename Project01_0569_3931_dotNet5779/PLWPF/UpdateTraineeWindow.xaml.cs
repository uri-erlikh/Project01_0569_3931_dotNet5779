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

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.TraineeGear = (BO.GearBox)this.traineeGearComboBox.SelectedIndex;

                if (int.TryParse(this.phoneTextBox.Text, out int number1) != true ||
                        int.TryParse(this.numOfBuildingTextBox.Text, out int number2) != true ||
                        int.TryParse(this.drivingLessonsNumTextBox.Text ,out int number3)!=true)
                    MessageBox.Show("please insert only digits for: phone and num of building", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (familyNameTextBox.Text == "" || privateNameTextBox.Text == "" || schoolTextBox.Text == "" ||
                        traineeGearComboBox.ItemsSource == null || cityTextBox.Text == "" || streetTextBox.Text == "")
                        MessageBox.Show("please fill all fields", "d.m.v.", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    else
                    {
                        bl.UpdateTrainee(trainee);
                        MessageBox.Show("update is Succeeded");
                        new TrainiesWindow().Show();
                        this.Close();
                    }
                }
            }
            catch (BO.InvalidDataException r)
            {
                MessageBox.Show(r.Message);
            }
            catch(KeyNotFoundException x)
            {
                MessageBox.Show(x.Message);

            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TrainiesWindow().Show();
            this.Close();
        }
    }
}
