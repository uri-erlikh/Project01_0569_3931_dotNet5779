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
    /// Interaction logic for MoreDatesWindow.xaml
    /// </summary>
    /// 
    public class Dates
    {
        string date;
        public List<DateTime> Times = new List<DateTime>();
        DateTime startDate = new DateTime();
        public DateTime StartDate { get; set; }
        DateTime endtDate = new DateTime();
        public DateTime EndDate { get; set; }
        public override string ToString()
        {
            foreach (var item in Times)
                date = item.Day + "." + item.Month + "." + item.Year + "   ";
            return date;
        }

    }
    //---------------------------------------
    public partial class MoreDatesWindow : Window
    {
        BO.Test test = new BO.Test();
        BL.IBL bl;
        Dates dates = new Dates();

        public MoreDatesWindow(BO.Test test1)
        {
            InitializeComponent();
            this.DataContext = dates;
            test = test1;
            bl = BL.BL_Factory.GetBL();
            dates.EndDate = DateTime.Now;
            dates.StartDate= DateTime.Now;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dates.Times = bl.GetDateOfTests(dates.StartDate, dates.EndDate, test);
                MoreDatesTextBlock.Text = dates.ToString();

            }
            catch (KeyNotFoundException a)
            {
                MessageBox.Show(a.Message);
                


            }
            catch (BO.InvalidDataException a)
            {
                MessageBox.Show(a.Message);
               

            }

        }
    }
}
