using CRUDBC32.Context;
using CRUDBC32.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Outlook = Microsoft.Office.Interop.Outlook;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace CRUDBC32
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyContext myContext = new MyContext();
        int supplierId;
        int itemId;
        string pass;
        int roleId;
        private int id;
        int totalprice, idtrans;
        string struck = "Id/t" + "Name/t" + "Price/t";
        List<TransactionItem> cart = new List<TransactionItem>();

        public object ButtonAdd_Click { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ShowData();


            comboBox.ItemsSource = myContext.Suppliers.ToList();
            comboBox1.ItemsSource = myContext.Item.ToList();
            comboBoxRole.ItemsSource = myContext.Roles.ToList();
            text_transactionId.Text = DateTimeOffset.Now.DateTime.ToString();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill Name");
                return;
            }
            else if (textBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill Email");
                return;
            }
            else
            {
                var cekEmail = myContext.Suppliers.Where(a => a.Email == textBox.Text).FirstOrDefault();
                if (cekEmail == null)
                {
                    var push = new Supplier(textBox1.Text, textBox.Text);
                    myContext.Suppliers.Add(push);
                    var result = myContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("row has been inserted");
                    }

                    dataGrid.ItemsSource = myContext.Suppliers.ToList();
                    try
                    {
                        Outlook._Application _app = new Outlook.Application();
                        Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                        //sesuaikan dengan content yang di xaml
                        mail.To = textBox.Text;
                        mail.Subject = "contoh wpf";//isi sendiri ini seperti subject pas mau ngirim email;
                        mail.Body = "Ini isi pesannya bebas mau buat apa";
                        mail.Importance = Outlook.OlImportance.olImportanceNormal;
                        ((Outlook._MailItem)mail).Send();
                        MessageBox.Show("Message has been sent.", "Message", MessageBoxButton.OK);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
                    }
                    textBox.Text = "";
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Email has Been Registered");
                }
            }
        }

        public void ShowData()
        {
            dataGrid.ItemsSource = myContext.Suppliers.ToList();
            dataGrid1.ItemsSource = myContext.Item.ToList();
        }


        private void dataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object data = dataGrid.SelectedItem;
                txtId.Text = (dataGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                textBox1.Text = (dataGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                textBox.Text = (dataGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z0-9.@]+$");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = Convert.ToInt32(txtId.Text);
                var dRow = myContext.Suppliers.Where(w => w.Id == num).FirstOrDefault();
                myContext.Suppliers.Remove(dRow);
                myContext.SaveChanges();
                dataGrid.ItemsSource = myContext.Suppliers.ToList();
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(txtId.Text);
            var uRow = myContext.Suppliers.Where(t => t.Id == num).FirstOrDefault();
            myContext.SaveChanges();
            uRow.Name = textBox1.Text;
            uRow.Email = textBox.Text;
            dataGrid.ItemsSource = myContext.Suppliers.ToList();

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (text_Name.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill Name");
                return;
            }
            else if (text_Stock.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill Stock");
                return;
            }
            else if (text_Price.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill Price");
                return;
            }
            else
            {
                {
                    var supplier = myContext.Suppliers.Where(s => s.Id == supplierId).FirstOrDefault();
                    var push = new Item(text_Name.Text, Convert.ToInt32(text_Stock.Text), Convert.ToInt32(text_Price.Text), supplier);
                    myContext.Item.Add(push);
                    var result = myContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("row has been inserted");
                    }
                    ShowData();
                    //text_Id.Text = "";
                    text_Id.Text = "";
                    text_Name.Text = "";
                    text_Stock.Text = "";
                    text_Price.Text = "";

                }
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object data = dataGrid1.SelectedItem;
                text_Id.Text = (dataGrid1.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                text_Name.Text = (dataGrid1.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                text_Stock.Text = (dataGrid1.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                text_Price.Text = (dataGrid1.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(text_Id.Text);
            var uRow = myContext.Item.Where(t => t.Id == supplierId).FirstOrDefault();
            var uRoww = myContext.Item.FirstOrDefault(i => i.Id == id);
            myContext.SaveChanges();
            uRow.Name = text_Name.Text;
            uRow.Stock = Convert.ToInt32(text_Stock.Text);
            uRow.Price = Convert.ToInt32(text_Price.Text);
            dataGrid1.ItemsSource = myContext.Item.ToList();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            supplierId = Convert.ToInt32(comboBox.SelectedValue.ToString());
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = Convert.ToInt32(text_Id.Text);
                var dRow = myContext.Item.Where(w => w.Id == num).FirstOrDefault();
                myContext.Item.Remove(dRow);
                myContext.SaveChanges();
                dataGrid.ItemsSource = myContext.Item.ToList();

            }

            catch (Exception)
            {

            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemId = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            var namedul = myContext.Item.FirstOrDefault(w => w.Id == itemId);
            text_price.Text = namedul.Price.ToString();
            txtstk.Text = namedul.Stock.ToString();

        }

        private void text_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            //totalprice = totalprice + 
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string iditem = itemId.ToString();
            int price = Convert.ToInt32(text_price.Text);
            int qua = Convert.ToInt32(text_quantity.Text);
            int total = price * qua;
            int subtot = price * qua;

            idtrans = Convert.ToInt32(textId_transaction.Text.ToString());

            var trans = myContext.Transactions.Where(b => b.Id == idtrans).FirstOrDefault();
            var prod = myContext.Item.Where(p => p.Id == itemId).FirstOrDefault();

            cart.Add(new TransactionItem { Transactions = trans, Items = prod, Quantity = Convert.ToInt32(text_quantity.Text) });
            totalprice += subtot;
            text_total.Text = totalprice.ToString();

            dataGrid2.Items.Add(new { Id = iditem, TransactionDate = text_transactionId.Text, Items = comboBox1.Text, Quantity = text_quantity.Text, Price = text_price.Text, Total = total.ToString() });
        }

        private void text_total_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text_pay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            int totalharga = Convert.ToInt32(text_total.Text);
            int pay = Convert.ToInt32(text_pay.Text);
            if (text_pay.Text == "")
            {
                MessageBox.Show("Required", "Caution");
            }
            else if (totalharga <= pay)
            {
                int transacId = Convert.ToInt32(textId_transaction.Text);
                var item = myContext.TransactionItem.FirstOrDefault(i => i.Transactions.Id == transacId);
                var trans = myContext.Transactions.FirstOrDefault(h => h.Id == transacId);
                int totprice = Convert.ToInt32(text_total.Text);
                trans.Total = totprice;
                foreach (var transItem in cart)
                {
                    myContext.TransactionItem.Add(transItem);
                    myContext.SaveChanges();
                    struck = transItem.Transactions.Id + "/t" + transItem.Items.Name + "/t" + transItem.Transactions.Total + "/t";
                }
                totalprice = 0;
                MessageBox.Show("OK");
            }
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString(struck, font, PdfBrushes.Black, new PointF(0, 0));

                //Save the document
                document.Save("Output.pdf");
                //Message box confirmation to view the created document.
                if (MessageBox.Show("Do you want to view the PDF?", "PDF has been created",
                    MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    try
                    {
                        //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                        System.Diagnostics.Process.Start("Output.pdf");

                        //Exit
                        Close();
                    }
                    catch (Win32Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else
                    Close();

            }

        }

        private void text_pay_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int pay = Convert.ToInt32(text_pay.Text);
                int totPrice = Convert.ToInt32(text_total.Text);
                TxtChange.Text = (pay - totPrice).ToString();
            }
            catch (Exception)
            {
            }

        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid2.SelectedItem != null)
            {
                dataGrid2.Items.Remove(dataGrid2.SelectedItem);
            }
        }

        private void ComboBoxRole_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            roleId = Convert.ToInt32(comboBoxRole.SelectedValue.ToString());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtNameRegister.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill Name");
                return;
            }
            else if (txtEmailRegister.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill Email");
                return;
            }
            else
            {
                var cekEmail = myContext.Users.Where(a => a.Email == txtEmailRegister.Text).FirstOrDefault();
                pass = Guid.NewGuid().ToString();
                var role = myContext.Roles.Where(c => c.ID == roleId).FirstOrDefault();

                if (cekEmail == null)
                {
                    //var useru = myContext.Roles.Where(s => s.ID == roleId).FirstOrDefault();
                    var push = new User(txtNameRegister.Text, txtEmailRegister.Text, pass, role);
                    myContext.Users.Add(push);
                    var result = myContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("row has been inserted");
                    }
                    try
                    {
                        Outlook._Application _app = new Outlook.Application();
                        Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                        //sesuaikan dengan content yang di xaml
                        mail.To = txtEmailRegister.Text;
                        mail.Subject = "New Registered Account Shooping Cart App";//isi sendiri ini seperti subject pas mau ngirim email;
                        mail.Body = "Hii There!" + "/n" + "Use This Password to Login in Shooping Cart App: " + pass;
                        mail.Importance = Outlook.OlImportance.olImportanceNormal;
                        ((Outlook._MailItem)mail).Send();
                        MessageBox.Show("Message has been sent.", "Message", MessageBoxButton.OK);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
                    }
                    ShowData();
                    txtNameRegister.Text = "";
                    txtEmailRegister.Text = "";
                    
                }
                else
                {
                    MessageBox.Show("Email has Been Registered");
                }

            
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var push = new Transaction();
            myContext.Transactions.Add(push);
            myContext.SaveChanges();
            textId_transaction.Text = Convert.ToString(push.Id);

        }
    }
}
