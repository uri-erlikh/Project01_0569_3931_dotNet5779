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
    /// Interaction logic for UpdateTestWindow.xaml
    /// </summary>
    public partial class UpdateTestWindow : Window
    {
        BO.Test test1;
        BL.IBL bl;

        public UpdateTestWindow(string number, BO.Test test)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bl = BL.BL_Factory.GetBL();
            try
            {
                test1 = bl.GetOneTest(test.TestNumber);
            }
            catch(KeyNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }
            this.DataContext = test1;
            //NumOfTestWindow numOfTestWindow = new NumOfTestWindow();
            // this.DataContext = test;

           
            numTestTextBlock.Text = "number of test: " + number;

            // numtesttextblock.Text= NumOfTestWindow.
        }
        //--------------------------------------------------------------
        private void UpdateTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateTestResult(test1);
                MessageBox.Show("update succeeded","d.m.v.",MessageBoxButton.OK,MessageBoxImage.Information);
                new TestsWindow().Show();
                this.Close();
            }
            catch(KeyNotFoundException t)
            {
                MessageBox.Show(t.Message);
            }
            catch (ArgumentNullException x)
            {
                MessageBox.Show(x.Message);
            }
            catch (BO.InvalidDataException r)
            {
                MessageBox.Show(r.Message);
            }
        }
        //--------------------------------------------------------------------
        private void GObutton_Click(object sender, RoutedEventArgs e)
        {
            if (this.IDTestertextBox.Text.Length < 9)
                MessageBox.Show("please insert valid ID - 9 digits", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (int.TryParse(this.IDTestertextBox.Text, out int number) != true)
                {
                    MessageBox.Show("Please enter only numbers", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.IDTestertextBox.Clear();
                }
                else if (test1.Tester.ID != this.IDTestertextBox.Text)
                {
                    MessageBox.Show("You aren't allowed to update the test", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Stop);
                    this.IDTestertextBox.Clear();
                }
                else
                {
                    this.updateTestButton.Visibility = Visibility.Visible;
                    this.grid1.IsEnabled = true;
                }
            }
        }
        //-----------------------------------------------------------------------
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TestsWindow().Show();
            this.Close();
        }        
    }
}
