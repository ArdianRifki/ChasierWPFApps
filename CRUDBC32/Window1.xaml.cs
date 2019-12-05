using CRUDBC32.Context;
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
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;


namespace CRUDBC32
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        MyContext myContext = new MyContext();
        public Window1()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = myContext.Users.Where(i => i.Email == txtEmail.Text).FirstOrDefault();

                if ((txtEmail.Text == "") || (txtPassword.Password == ""))
                {
                    if (txtEmail.Text == "")
                    {
                        MessageBox.Show("Email is Required", "Caution", MessageBoxButton.OK);
                        txtEmail.Focus();
                    }

                    else if (txtPassword.Password == "")
                    {
                        MessageBox.Show("Password is Required", "Caution", MessageBoxButton.OK);
                        txtPassword.Focus();
                    }
                }
                else
                {
                    //if (email is null)
                    //{
                    //    var dpp = email.Password;
                    //    dpp = txtPassword.Password;
                    //    if (txtPassword.Password == dpp)
                    //    {
                    //        MessageBox.Show("Login Succesfully", "Login Succes", MessageBoxButton.OK);
                    //        MainWindow dashboard = new MainWindow();
                    //        dashboard.Show();
                    //        this.Close();
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Email and Password are wrong");
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Email and Password is Invalid");
                    //}
                    var dpp = email.Password;
                    dpp = txtPassword.Password;
                    if (txtPassword.Password == dpp)
                    {
                        MessageBox.Show("Login Succesfully", "Login Succes", MessageBoxButton.OK);
                        MainWindow dashboard = new MainWindow();
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Email and Password are wrong");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Forgot dashboard = new Forgot();
            dashboard.Show();
            this.Close();
        }
    }
}

