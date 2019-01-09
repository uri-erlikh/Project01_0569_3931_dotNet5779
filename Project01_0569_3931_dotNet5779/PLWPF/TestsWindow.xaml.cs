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
        public TestsWindow()
        {
            InitializeComponent();
        }

        private void ToAddaTest_Click(object sender, RoutedEventArgs e)
        {
            new AddTestWindow().Show();
            this.Close();
        }

        private void ToUpdateTestResult_Click(object sender, RoutedEventArgs e)
        {
            new NumOfTestWindow("update").Show();
        }
        
        private void ToPrintTest_Click(object sender, RoutedEventArgs e)
        {
            new NumOfTestWindow("print").Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {            
            new MainWindow().Show();
            this.Close();
        }

        private void ToDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            new NumOfTestWindow("delete").Show();
        }
    }
}
