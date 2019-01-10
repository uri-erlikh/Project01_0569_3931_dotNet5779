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
    /// Interaction logic for NumOfTestWindow.xaml
    /// </summary>
    public partial class NumOfTestWindow : Window
    {
        string value1;
        public BO.Test test = new BO.Test();
        BL.IBL bl;

        public NumOfTestWindow(string value)
        {
            InitializeComponent();
            value1 = value;
            bl = BL.BL_Factory.GetBL();
        }


        private void NumOfTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.numOfTestTextBox.Text.Length < 8)
                MessageBox.Show("please insert valid number - 8 digits", "d.m.v.");
            bool check = int.TryParse(numOfTestTextBox.Text, out int num);
            if (!check)
            {
                MessageBox.Show("enter only numbers");
                numOfTestTextBox.Clear();
            }

            switch (value1)
            {
                case "update":
                    try
                    {
                        test = bl.GetOneTest(num);
                        new UpdateTestWindow(numOfTestTextBox.Text, test).Show();
                        this.Close();
                    }
                    catch (KeyNotFoundException r)
                    {
                        MessageBox.Show(r.Message);
                        numOfTestTextBox.Clear();
                    }                    
                    break;
                case "delete":
                    try
                    {
                        test = bl.GetOneTest(num);
                        new PrintTestWindow(numOfTestTextBox.Text, test).Show();
                        this.Close();
                    }
                    catch (KeyNotFoundException r)
                    {
                        MessageBox.Show(r.Message);
                        numOfTestTextBox.Clear();
                    }            
                    break;
                case "print":                    
                try
                {
                    test = bl.GetOneTest(num);
                    new PrintTestWindow(numOfTestTextBox.Text, test).Show();
                    this.Close();
                }
                catch (KeyNotFoundException r)
                {
                    MessageBox.Show(r.Message);
                        numOfTestTextBox.Clear();
                }            
                break;
        }

    }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new TestsWindow().Show();
            this.Close();
        }
    }
}


//}




