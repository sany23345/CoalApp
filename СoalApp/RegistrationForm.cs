using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СoalApp
{
    public partial class RegistrationForm : Form
    {
        public  Form formParent;
        public string regAddress;
        static string connect = @"Data Source=DESKTOP-DJUDJM1\SQLEXPRESS;Initial Catalog=BD_Coal;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(connect);

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            formParent.Visible = true;
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            formParent.Visible = true;
        }

        private void passwordAgainTextBox_TextChanged(object sender, EventArgs e)
        {
            if (passwordTextBox.Text==passwordAgainTextBox.Text)
            {
                pictureBox1.Image = Properties.Resources.checkmark;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.x;
            }
        }

        private void mapButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty( addressTextBox.Text))
            {
                DialogResult dialogResu = MessageBox.Show("Адрес уже записан, желаете изменить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResu==DialogResult.Yes)
                {
                    regAddress = "";
                    MapForm mapForm = new MapForm();
                    mapForm.regPrentForm = this;
                    mapForm.Visible = true;
                    mapForm.markForm = "регистрация";
                    this.Visible = false;
                }        
            }
            else
            {
                MapForm mapForm = new MapForm();
                mapForm.regPrentForm = this;
                mapForm.markForm = "регистрация";
                mapForm.Visible = true;
                this.Visible = false;
            }                                   
        }

        private void RegistrationForm_VisibleChanged(object sender, EventArgs e)
        {
            addressTextBox.Text = regAddress;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(addressTextBox.Text) && !string.IsNullOrEmpty(surnameTextBox.Text) && !string.IsNullOrEmpty(nameTextBox.Text) && !string.IsNullOrEmpty(patronymicTextBox.Text) && !string.IsNullOrEmpty(telTextBox.Text))
            {
                if (passwordTextBox.Text == passwordAgainTextBox.Text)
                {
                    sqlConnection.Open();
                    string zapros = @"Insert Into  Клиенты  values('" + telTextBox.Text + "','" + surnameTextBox.Text + " " + nameTextBox.Text + " " + patronymicTextBox.Text + "','" + addressTextBox.Text + "','" + passwordAgainTextBox.Text + "')";
                    SqlCommand sqlCommand = new SqlCommand(zapros, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show("Реистрация прошла успешно");
                    this.Visible = false;
                    formParent.Visible = true;
                }
                else
                {
                    MessageBox.Show("Пароли не совподают!!!");
                }            
            }
            else
            {
                MessageBox.Show("Введите все данные!!!");
            }
        }

        private void telTextBox_TextChanged(object sender, EventArgs e)
        {
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

        private void telTextBox_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        public void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
