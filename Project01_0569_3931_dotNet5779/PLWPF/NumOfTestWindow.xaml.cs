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
            // this.DataContext = test;
            bl = BL.BL_Factory.GetBL();
        }


        private void Numoftestbutton_Click(object sender, RoutedEventArgs e)
        {           
                if (this.numoftesttextbox.Text.Length < 8)
                MessageBox.Show("please insert valid number - 8 digits", "d.m.v.");
                switch (value1)
                {
                    case "update":
                        int num;
                        bool check = int.TryParse(numoftesttextbox.Text, out num);
                        if (check)
                        {
                            try
                            {
                                test = bl.GetOneTest(num);
                                new UpdateTestWindow(numoftesttextbox.Text, test).Show();
                                // updateTestWindow.Show();
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
                        break;
                    case "delete":

                        check = int.TryParse(numoftesttextbox.Text, out num);
                        if (check)
                        {
                            try
                            {
                                test = bl.GetOneTest(num);
                                new PrintTestWindow(numoftesttextbox.Text, test).Show();
                                // updateTestWindow.Show();
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
                        break;
                    case "print":
                        check = int.TryParse(numoftesttextbox.Text, out num);
                        if (check)
                        {
                            try
                            {
                                test = bl.GetOneTest(num);
                                new PrintTestWindow(numoftesttextbox.Text, test).Show();
                                // updateTestWindow.Show();
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
                        break;
                }

            }
        }
    }


//}




