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
    /// Interaction logic for Testers.xaml
    /// </summary>
    public partial class TestersWindow : Window
    {
        BO.Tester tester = new BO.Tester();
        BL.IBL bl;
        string testerID;

        public TestersWindow()
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
        }

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
                    testerID = GetIDTextBox.Text;
                    tester = bl.GetOneTester(GetIDTextBox.Text);
                    this.UpdateTesterButton.IsEnabled = true;
                    this.DeleteTesterButton.IsEnabled = true;
                    this.PrintTesterButton.IsEnabled = true;
                    this.GetTestOfTTesterButton.IsEnabled = true;
                }
                catch (KeyNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BO.InvalidDataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //--------------------------------------------------------------------------
        private void AddTesterButton_Click(object sender, RoutedEventArgs e)
        {
            new AddTesterWindow().Show();
            this.Close();
        }
        //---------------------------------------------------------------------------
        private void UpdateTesterButton_Click(object sender, RoutedEventArgs e)
        {
            //new UpdateTesterWindow().Show();
            this.Close();
        }
        //-------------------------------------------------------------------------
        private void DeleteTesterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Are you sure you want to delete the tester?", "d.m.v.", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bl.DeleteTester(testerID);
                MessageBox.Show("Tester was deleted from list", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Information);
                reset();                
            }
        }
        //------------------------------------------------------------------------------
        private void PrintTesterButton_Click(object sender, RoutedEventArgs e)
        {
            DataTextBlock.Visibility = Visibility;
            DataTextBlock.Background = Brushes.DarkSeaGreen;
            DataTextBlock.Text = tester.ToString();
        }
        //-------------------------------------------------------------------
        private void GetTestOfTTesterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTextBlock.Visibility = Visibility;
                DataTextBlock.Background = Brushes.Gold;
                DataTextBlock.Text = "";
                List<BO.TesterTest> list = bl.GetFutureTestForTester(testerID);
                if (!list.Any())
                    DataTextBlock.Text = "no tests";
                else
                    foreach (var item in list)
                    {
                        DataTextBlock.Text += item.ToString() + "\n";
                    }
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
        //------------------------------------------------------------
        private void reset()
        {
            this.DataTextBlock.Visibility = Visibility.Hidden;
            this.GetIDTextBox.Clear();
            this.PrintTesterButton.IsEnabled = false;
            this.GetTestOfTTesterButton.IsEnabled = false;
            this.DeleteTesterButton.IsEnabled = false;
            this.UpdateTesterButton.IsEnabled = false;
        }
    }
}



