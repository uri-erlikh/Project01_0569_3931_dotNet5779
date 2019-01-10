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
    /// Interaction logic for UpdateTesterWindow.xaml
    /// </summary>
    public partial class UpdateTesterWindow : Window
    {
        BL.IBL bl;
        BO.Tester tester = new BO.Tester();
        public UpdateTesterWindow(BO.Tester tester1)
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
            tester = tester1;
            numTesterTextBlock.Text = "ID: " + tester.ID;
            this.DataContext = tester;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (int.TryParse(this.phoneTextBox.Text, out int number1) != true ||
                        int.TryParse(this.numOfBuildingTextBox.Text, out int number2) != true)
                    MessageBox.Show("please insert only digits for: phone and num of building", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    bl.UpdateTester(tester);
                    MessageBox.Show("update is Succeeded");
                    new TestersWindow().Show();
                    this.Close();
                }
            }
            catch (BO.InvalidDataException r)
            {
                MessageBox.Show(r.Message);
            }
            catch (KeyNotFoundException x)
            {
                MessageBox.Show(x.Message);

            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TestersWindow().Show();
            this.Close();
        }
    }
}
