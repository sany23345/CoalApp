using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        public ConfirmationForm()
        {
            InitializeComponent();
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
            }
            else
            {
                MessageBox.Show("Введите номер телефона!!!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == password.ToString())
            {
                MessageBox.Show("Ваш заказ принят!!!");

                //string zakaz =  "Заказ угля:"+"\n" +
                //                "Поставщик: " +provider+"\n"+
                //                "Марка угля: "+stamp + "\n"+
                //                "Требуемое количество(тонн): "+requiredWeight + "\n" +
                //                "Адрес доставки: "+address;

                string zakaz =  "Заказ угля: " +
                                " Поставщик: " + provider +
                                " Марка угля: " + stamp +
                                " Требуемое количество(тонн): " + requiredWeight +
                                " Адрес доставки: " + address;
                SMSC smsc = new SMSC();
                string[] r = smsc.send_sms(telTextBox.Text, zakaz, 1);
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
