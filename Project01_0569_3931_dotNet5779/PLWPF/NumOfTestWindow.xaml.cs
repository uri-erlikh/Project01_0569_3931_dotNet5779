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
        public BO.Test test = new BO.Test();
        BL.IBL bl;
        public NumOfTestWindow()
        {
            InitializeComponent();

            // this.DataContext = test;
            bl = BL.BL_Factory.GetBL();

        }
        //
        private void Searchnumoftest_Click(object sender, RoutedEventArgs e)
        {

            int num;
            bool check = int.TryParse(numoftesttextbox.Text, out num);
            if (check)
            {
                try
                {
                    test = bl.GetOneTest(num);
                    UpdateTestWindow updateTestWindow = new UpdateTestWindow(numoftesttextbox.Text);
                    updateTestWindow.Show();
                    this.Close();
                }
                catch (KeyNotFoundException r)
                {
                    MessageBox.Show(r.Message);
                    numoftesttextbox.Clear();
                }
            }
            else
            {
                MessageBox.Show("enter only numbers");
                numoftesttextbox.Clear();
            }
        }
    }
}

