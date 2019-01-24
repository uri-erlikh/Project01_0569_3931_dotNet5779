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
    /// Interaction logic for Testers.xaml
    /// </summary>
    public partial class TestersWindow : Window
    {
        BO.Tester tester;
        BL.IBL bl;
        string testerID;
        ObservableCollection<BO.Tester> testers = new ObservableCollection<BO.Tester>();
        ObservableCollection<string> _testersID = new ObservableCollection<string>();//
        bool check = true;
        public TestersWindow(string identifier)
        {
            InitializeComponent();
            bl = BL.BL_Factory.GetBL();
            if (identifier == "admin")
            {
                this.AddTesterButton.Visibility = Visibility.Visible;
                this.UpdateTesterButton.Visibility = Visibility.Visible;
                this.DeleteTesterButton.Visibility = Visibility.Visible;
            }

            if (BO.Tester.testersRecentlyopened.Any())
            {
                foreach (var item in BO.Tester.testersRecentlyopened)
                {
                    _testersID.Add(item.ID);
                    ListBoxItem listBoxItem = new ListBoxItem();
                    listBoxItem.Content = item;
                    DetailsListBox.Items.Add(listBoxItem);
                }
            }           
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
                    if (!testers.Any() || testers[0].ID != tester.ID)
                    {
                        testers.Clear();
                        testers.Add(tester);
                      //  BO.Tester.testersRecentlyopened.Add(tester);
                        foreach (var item in _testersID)
                        {
                            if (testerID == item)
                                check = false;
                        }
                        if (check)
                        {
                            BO.Tester.testersRecentlyopened.Add(tester);
                            _testersID.Add(tester.ID);
                            ListBoxItem listBoxItem = new ListBoxItem();
                            listBoxItem.Content = tester;
                            DetailsListBox.Items.Add(listBoxItem);
                        }
                    }

                    this.PrintTesterButton.IsEnabled = true;
                    this.GetTestOfTTesterButton.IsEnabled = true;
                    if (this.AddTesterButton.Visibility == Visibility.Visible)
                    {
                        this.UpdateTesterButton.IsEnabled = true;
                        this.DeleteTesterButton.IsEnabled = true;
                    }
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
            new UpdateTesterWindow(tester).Show();
            this.Close();
        }
        //-------------------------------------------------------------------------
        private void DeleteTesterButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //------------------------------------------------------------------------------
        private void PrintTesterButton_Click(object sender, RoutedEventArgs e)
        {
            this.DetailsTestListView.Visibility = Visibility.Hidden;
            this.DetailsTesterListView.Visibility = Visibility.Visible;
            this.DetailsTesterListView.ItemsSource = testers;
            //  DataTextBlock.Visibility = Visibility;
            // DataTextBlock.Background = Brushes.DarkSeaGreen;
            //DataTextBlock.Text = tester.ToString();
        }
        //-------------------------------------------------------------------
        private void GetTestOfTTesterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DetailsTesterListView.Visibility = Visibility.Hidden;

                // DataTextBlock.Text = "";
                List<BO.TesterTest> list = bl.GetFutureTestForTester(testerID);
                if (!list.Any())
                {
                    this.DataTextBlock.Visibility = Visibility;
                    this.DataTextBlock.Background = Brushes.Gold;
                    this.DataTextBlock.Text = "no tests";
                }
                else
                {
                    this.DetailsTestListView.Visibility = Visibility.Visible;
                    this.DetailsTestListView.ItemsSource = list;
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
            this.DetailsTesterListView.Visibility = Visibility.Hidden;
            this.DataTextBlock.Visibility = Visibility.Hidden;
            this.GetIDTextBox.Clear();
            testers.Clear();
            this.PrintTesterButton.IsEnabled = false;
            this.GetTestOfTTesterButton.IsEnabled = false;
            this.DeleteTesterButton.IsEnabled = false;
            this.UpdateTesterButton.IsEnabled = false;
        }
        //------------------------------------------------------------------------------
        private void DetailsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reset();
            testerID = _testersID[DetailsListBox.SelectedIndex];
            GetIDTextBox.Text = testerID;
            try
            {
                tester = bl.GetOneTester(GetIDTextBox.Text);
                testers.Add(tester);
                this.PrintTesterButton.IsEnabled = true;
                this.GetTestOfTTesterButton.IsEnabled = true;
                if (this.AddTesterButton.Visibility == Visibility.Visible)
                {
                    this.UpdateTesterButton.IsEnabled = true;
                    this.DeleteTesterButton.IsEnabled = true;
                }
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
}



