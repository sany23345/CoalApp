using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СoalApp
{
    public partial class ConfirmationForm : Form
    {
        public Form parentForm;

        public string provider, stamp, distance, address, pricePerTon,
                      requiredWeight, fullPrice, shippingCost,tel;
        int password;
        static string connect = "Data Source=bd_coal.db;Version=3";
        SQLiteConnection sQLiteConnection = new SQLiteConnection(connect);
    

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Поле ввода телефона.При начала вводе если начинает вводить с 8, преобразуется в +7
            //если с +, то подставляется +7, если ввод с 7, то подставляется +7, любая другая цифра, то подставляется +7
            //вводятся только цифры
            //+7 983 278 45 67

            try
            {
                if (telTextBox.Text == "8")
                    telTextBox.Text = "+7";
                if (telTextBox.Text == "+")
                    telTextBox.Text = "+7";
                if (telTextBox.Text == "7")
                    telTextBox.Text = "+7";
                if (int.Parse(telTextBox.Text) >= 0 && int.Parse(telTextBox.Text) <= 9)
                    telTextBox.Text = "+7";
                telTextBox.SelectionStart = telTextBox.Text.Length;
            }
            catch (Exception) { }
        }

        private void ConfirmationForm_VisibleChanged(object sender, EventArgs e)
        {
            providerTextBox.Text = provider;
            stampTextBox.Text = stamp;
            distanceTextBox.Text = distance;
            addressTextBox.Text = address;
            pricePerTonTextBox.Text = pricePerTon;
            requiredWeightTextBox.Text = requiredWeight;
            fullPriceTextBox.Text = fullPrice;
            shippingCostTextBox.Text = shippingCost;
            telTextBox.Text = tel;
            textBox10.Enabled = false;
        }

        public ConfirmationForm()
        {
            InitializeComponent();
            //string path = Path.Combine(Directory.GetParent(Application.StartupPath).FullName, "Debug\\Database1.mdf");
            //connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=30";
            //sqlConnection = new SqlConnection(connect);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (telTextBox.Text.Length == 12)
            {
                Random random = new Random();
                password = random.Next(10000, 99999);
                SMSC smsc = new SMSC();
                string[] r = smsc.send_sms(telTextBox.Text, "Код для подтверждения заказа: " + password, 1);
                MessageBox.Show("Код для подтверждения отправлен.");
                textBox10.Enabled = true;
            }
            else
            {
                MessageBox.Show("Введите номер телефона!!!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox10.Text  ==  password.ToString())
            {
                string zakaz = "Заказ угля: " +
                                " Поставщик: " + provider +
                                " Марка угля: " + stamp +
                                " Требуемое количество(тонн): " + requiredWeight +
                                " Адрес доставки: " + address;
                SMSC smsc = new SMSC();
                string[] r = smsc.send_sms("+79628003000", zakaz, 1);

                string zapros = @"Select Поставляемый_уголь.id,Марки_угля.Наименование_марки,Поставщики.Наименование from Поставляемый_уголь
                    inner join Марки_угля on Марки_угля.id=Поставляемый_уголь.id_Угля
                    inner join Поставщики on Поставщики.id=Поставляемый_уголь.id_Поставщика
                    where Поставщики.Наименование='"+providerTextBox.Text+"' and Марки_угля.Наименование_марки='"+stampTextBox.Text+"'";
                DataTable dtProvCoalId = new DataTable();
                SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(zapros, sQLiteConnection);
                sqlDataAdapter.Fill(dtProvCoalId);
                string provCoalId = dtProvCoalId.Rows[0][0].ToString();
              

                zapros = @"select MAX(id) from Заказы";
                DataTable dtZakazId = new DataTable();
                sqlDataAdapter = new SQLiteDataAdapter(zapros, sQLiteConnection);
                sqlDataAdapter.Fill(dtZakazId);
                int zakazId = int.Parse(dtZakazId.Rows[0][0].ToString());
                zakazId++;

                zapros = @"Insert Into Заказы(id,Номер_телефона_клиента,Дата_заказа,Общая_цена,Адресс_доставки,Сумма_доставки) values('" + zakazId + "','" + telTextBox.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:00") + "','" + fullPriceTextBox.Text + "','" + addressTextBox.Text + "','" + shippingCostTextBox.Text + "');" +
                           " insert into Содержимое_заказа(id_Заказа,id_поставляемого_угля,Количество_тонаж,Сумма) values('" + zakazId + "', '" + provCoalId + "','" + requiredWeightTextBox.Text + "','" + double.Parse(requiredWeightTextBox.Text) * double.Parse(pricePerTonTextBox.Text) + "');" ;

                SQLiteCommand sqlCommand = new SQLiteCommand(zapros, sQLiteConnection);
                sQLiteConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sQLiteConnection.Close();

                MessageBox.Show("Ваш заказ принят!!!");
                this.Close();
                parentForm.Visible = true;
            }
            else
            {
                MessageBox.Show("Введеный код не правильный!!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            parentForm.Visible = true;
        }

        private void ConfirmationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Visible = true;
        }
    }
}
