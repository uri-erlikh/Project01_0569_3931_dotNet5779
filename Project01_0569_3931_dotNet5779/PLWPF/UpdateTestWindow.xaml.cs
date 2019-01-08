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
      //  BO.Test test = new BO.Test();
        BL.IBL bl;

        public UpdateTestWindow(string number)
        {
            InitializeComponent();
            //NumOfTestWindow numOfTestWindow = new NumOfTestWindow();
           // this.DataContext = test;

            bl = BL.BL_Factory.GetBL();
            numtesttextblock.Text = "number of test: " + number;

           // numtesttextblock.Text= NumOfTestWindow.
        }

        private void Updatetestbutton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //   // bl.UpdateTestResult()
            //}
            //catch
        }
    }
}
