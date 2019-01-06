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
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTestWindow : Window
    {
        BO.Test test = new BO.Test();
        BL.IBL bl;

        public AddTestWindow()
        {
            InitializeComponent();
            
            this.DataContext = test;
            
            bl = BL.BL_Factory.GetBL();

            this.vehicleComboBox.ItemsSource = Enum.GetValues(typeof(BO.Vehicle));
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddTest(test);
                test = new BO.Test();
                this.DataContext = test;
            }
            catch () { }
        }
    }
}
