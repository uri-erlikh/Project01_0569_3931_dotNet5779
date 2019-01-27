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
using System.Collections.ObjectModel;


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
        ObservableCollection<BO.Test> tests = new ObservableCollection<BO.Test>();
        ObservableCollection<string> _Numtest = new ObservableCollection<string>();//
        bool check = true;
        public TestsWindow()
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
            DataContext = test;

            if (BO.Test.testsRecentlyOpened.Any())
            {
                foreach (var item in BO.Test.testsRecentlyOpened)
                {
                    _Numtest.Add(item.TestNumber.ToString());
                    ListBoxItem listBoxItem = new ListBoxItem();
                    listBoxItem.Content = item;
                    DeatielsListBox.Items.Add(listBoxItem);
                }
            }
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
                    if (!tests.Any() || tests[0].TraineeId != test.TraineeId || tests[0].Vehicle != test.Vehicle)
                    {
                        tests.Clear();
                        tests.Add(test);
                       // BO.Test.testsRecentlyopened.Add(test);
                        foreach (var item in _Numtest)
                        {
                            if (testNumber == int.Parse(item))
                                check = false;
                        }
                        if (check)
                        {
                            if (BO.Test.testsRecentlyOpened.Count < 5)
                            {
                                BO.Test.testsRecentlyOpened.Enqueue(test);
                                _Numtest.Add(GetTestNumTextBox.Text);
                                ListBoxItem listBoxItem = new ListBoxItem();
                                listBoxItem.Content = test;
                                DeatielsListBox.Items.Add(listBoxItem);
                            }
                            else
                            {
                                BO.Test.testsRecentlyOpened.Dequeue();
                                BO.Test.testsRecentlyOpened.Enqueue(test);
                                DeatielsListBox.Items.Clear();
                                _Numtest.Clear();
                                foreach(var item in BO.Test.testsRecentlyOpened)
                                {
                                    _Numtest.Add(item.TestNumber.ToString());
                                    ListBoxItem listBoxItem = new ListBoxItem();
                                    listBoxItem.Content = item;
                                    DeatielsListBox.Items.Add(listBoxItem);
                                }

                            }
                        }
                    }
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
            new AddTestWindow().Show();
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
            DetailsTestListView.Visibility = Visibility.Visible;
            // DataTextBlock.Visibility = Visibility;
            // DataTextBlock.Background = Brushes.DarkSeaGreen;
            //DataTextBlock.Text = test.ToString();
            DetailsTestListView.ItemsSource = tests;
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
            this.DetailsTestListView.Visibility = Visibility.Hidden;
         //   this.DataTextBlock.Visibility = Visibility.Hidden;
            this.GetTestNumTextBox.Clear();
            tests.Clear();
            this.PrintTestButton.IsEnabled = false;
            this.DeleteTestButton.IsEnabled = false;
            this.UpdateTestButton.IsEnabled = false;
        }
        //---------------------------------------------------------
        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            this.NumLabel.Visibility = Visibility.Visible;
            this.GetTestNumTextBox.Visibility = Visibility.Visible;
            this.GetDataButton.Visibility = Visibility.Visible;
            this.ResetButton.Visibility = Visibility.Visible;
            this.AddTestButton.Visibility = Visibility.Visible;
            this.UpdateTestButton.Visibility = Visibility.Visible;
            this.DeleteTestButton.Visibility = Visibility.Visible;
            this.PrintTestButton.Visibility = Visibility.Visible;
            this.passwordBox.Visibility = Visibility.Hidden;
            this.passwordLabel.Visibility = Visibility.Hidden;
            this.PasswordButton.Visibility = Visibility.Hidden;
            RecentlyopenedTab.Visibility = Visibility.Visible;
        }

        private void DeatielsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reset();
            try
            {
                testNumber = int.Parse(_Numtest[DeatielsListBox.SelectedIndex]);
            }
            catch (OverflowException ex)//יש פה המרה בגלל זה בוצע קטץ' לבדוק איזה קאטצים צריך להוסיף
            {
                MessageBox.Show(ex.Message);

            }
            GetTestNumTextBox.Text = _Numtest[DeatielsListBox.SelectedIndex];
            try
            {
                test = bl.GetOneTest(testNumber);
                tests.Add(test);
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

    //public TestsWindow()
    //{
    //    InitializeComponent();.
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
