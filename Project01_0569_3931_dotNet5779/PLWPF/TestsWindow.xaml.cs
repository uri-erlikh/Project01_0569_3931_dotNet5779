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
    /// Interaction logic for Tests.xaml
    /// </summary>
    public partial class TestsWindow : Window
    {
        BO.Test test = new BO.Test();
        BL.IBL bl;
        int testNumber;

        public TestsWindow()
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.GetTestNumTextBox.Text.Length < 8)
                MessageBox.Show("please insert valid number - 8 digits", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (int.TryParse(this.GetTestNumTextBox.Text, out int number) != true)
            {
                MessageBox.Show("please insert only digits for test-number", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Error);
                this.GetTestNumTextBox.Clear();
            }
            else
            {
                try
                {
                    testNumber = int.Parse(GetTestNumTextBox.Text);
                    test = bl.GetOneTest(testNumber);
                    this.UpdateTestButton.IsEnabled = true;
                    this.DeleteTestButton.IsEnabled = true;
                    this.PrintTestButton.IsEnabled = true;
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
        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            new AddTesterWindow().Show();
            this.Close();
        }
        //---------------------------------------------------------------------------
        private void UpdateTestButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdateTestWindow(test.TestNumber.ToString(), test).Show();
            this.Close();
        }
        //-------------------------------------------------------------------------
        private void DeleteTestButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Are you sure you want to delete the test?", "d.m.v.", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bl.DeleteTest(testNumber);
                MessageBox.Show("Test was deleted from list", "d.m.v.", MessageBoxButton.OK, MessageBoxImage.Information);
                reset();
            }
        }
        //------------------------------------------------------------------------------
        private void PrintTestButton_Click(object sender, RoutedEventArgs e)
        {
            DataTextBlock.Visibility = Visibility;
            DataTextBlock.Background = Brushes.DarkSeaGreen;
            DataTextBlock.Text = test.ToString();
        }
        //-------------------------------------------------------------------
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
            this.GetTestNumTextBox.Clear();
            this.PrintTestButton.IsEnabled = false;
            this.DeleteTestButton.IsEnabled = false;
            this.UpdateTestButton.IsEnabled = false;
        }
    }

    //public TestsWindow()
    //{
    //    InitializeComponent();
    //}



    //private void ToAddaTest_Click(object sender, RoutedEventArgs e)
    //{
    //    new AddTestWindow().Show();
    //    this.Close();
    //}

    //private void ToUpdateTestResult_Click(object sender, RoutedEventArgs e)
    //{
    //    new NumOfTestWindow("update").Show();
    //    this.Close();
    //}

    //private void ToPrintTest_Click(object sender, RoutedEventArgs e)
    //{
    //    new NumOfTestWindow("delete or print").Show();
    //    this.Close();
    //}

    //private void BackButton_Click(object sender, RoutedEventArgs e)
    //    {            
    //        new MainWindow().Show();
    //        this.Close();
    //    }


    //}
}
