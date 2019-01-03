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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestersButoon_Click(object sender, RoutedEventArgs e)
        {
            Window testersWindow = new TestersWindow();
            testersWindow.Show();
        }

        private void TrainiesButton_Click(object sender, RoutedEventArgs e)
        {
            Window trainiesWindow = new TrainiesWindow();
            trainiesWindow.Show();
        }

        private void TestsButton_Click(object sender, RoutedEventArgs e)
        {
            new TestsWindow().Show();            
        }
    }
}
