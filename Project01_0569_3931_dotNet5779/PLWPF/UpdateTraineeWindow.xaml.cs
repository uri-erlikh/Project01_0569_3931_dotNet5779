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
    public partial class UpdateTraineeWindow : Window
    {
        BL.IBL bl;
        BO.Trainee trainee = new BO.Trainee();
        public UpdateTraineeWindow(BO.Trainee trainee1)
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
            trainee = trainee1;
            this.DataContext = trainee;


        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateTrainee(trainee);
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
    }
}
