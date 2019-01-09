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
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        BO.Trainee myTrainee = new BO.Trainee();
        BL.IBL bl;

        public AddTraineeWindow()
        {
            InitializeComponent();

            myTrainee.DayOfBirth = DateTime.Now;
            this.DataContext = myTrainee;
            bl = BL.BL_Factory.GetBL();
            this.traineeVehicleComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
            this.traineeGearComboBox.ItemsSource = Enum.GetValues(typeof(BO.GearBox));
            this.personGenderComboBox.ItemsSource = Enum.GetValues(typeof(BO.Gender));
        }



    }
}
