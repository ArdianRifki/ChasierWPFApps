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
using Outlook = Microsoft.Office.Interop.Outlook;

namespace CRUDBC32
{
    /// <summary>
    /// Interaction logic for Forgot.xaml
    /// </summary>
    public partial class Forgot : Window
    {
        MyContext myContext = new MyContext();
        public Forgot()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 dashboard = new Window1();
            dashboard.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtForgetPassword.Text == "")
                {
                    MessageBox.Show("Email is Required", "Caution", MessageBoxButton.OK);
                    txtForgetPassword.Focus();
                }
                else
                {
                    var cekemail = myContext.Users.FirstOrDefault(p => p.Email == txtForgetPassword.Text);
                    if (cekemail != null)
                    {
                        var email = cekemail.Email;
                        if (txtForgetPassword.Text == email)
                        {
                            string newuser = Guid.NewGuid().ToString();
                            var emailcek = myContext.Users.Where(o => o.Email == txtForgetPassword.Text).FirstOrDefault();
                            emailcek.Password = newuser;
                            myContext.SaveChanges();
                            MessageBox.Show("Password has been updated");
                            Outlook._Application _app = new Outlook.Application();
                            Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                            //sesuaikan dengan content yang di xaml
                            mail.To = txtForgetPassword.Text;
                            mail.Subject = "[Forgot Password] " + DateTime.Now.ToString("ddMMyyyyhhmmss");
                            mail.Body = "Hii There!" + txtForgetPassword + "\n This is Your New Password: " + newuser;
                            mail.Importance = Outlook.OlImportance.olImportanceNormal;
                            ((Outlook._MailItem)mail).Send();
                            MessageBox.Show("Message has been sent.", "Message", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("That Email Not Registered", "Caution", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception)
            {
               
            }
        }
    }
}
