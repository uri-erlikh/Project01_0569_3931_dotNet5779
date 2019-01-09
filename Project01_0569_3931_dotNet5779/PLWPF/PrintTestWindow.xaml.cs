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
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for PrintTestWindow.xaml                           
    /// </summary>
    public partial class PrintTestWindow : Window
    {
        BO.Test test1 = new BO.Test();
        BL.IBL bl;
        
        public PrintTestWindow(string number, BO.Test test)
        {
            InitializeComponent();
            test1 = test;
            bl = BL.BL_Factory.GetBL();
            insertTestNumberTextBox.Text = "Number Test:" + number;
            GetTestTextBox.Text = test.ToString();
        }        
        //private void TestNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
            //    //if (e.Key == Key.Enter)
            //    //{
            //    //    if (this.TestNumberTextBox.Text.Length < 8)
            //    //        MessageBox.Show("please insert valid number - 8 digits", "d.m.v.");
            //    //    else if (int.TryParse(this.TestNumberTextBox.Text, out int number) != true)
            //    //    {
            //    //        MessageBox.Show("please insert only digits for testNumber", "d.m.v.");
            //    //        this.TestNumberTextBox.Clear();
            //    //    }
            //    //else
            //    //{
            //try
            //{
                //this.GetTestTextBox.Text =
                //this.GetTestTextBox.Visibility = Visibility.Visible;
              
           // }
            //catch (KeyNotFoundException a)
            //{
            //    MessageBox.Show(a.Message);
            //}

      //  }

        private void Deletebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteTest(test1.TestNumber);
                new TestsWindow().Show();
                this.Close();
            }
            catch (KeyNotFoundException r)
            {
                MessageBox.Show(r.Message);
            }
        }


        private void BackToSweetHome_Click(object sender, RoutedEventArgs e)
        {
            new TestsWindow().Show();
            this.Close();
        }
    }
}
