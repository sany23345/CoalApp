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
    public partial class AuthorizationForm : Form
    {
        static string connect = "Data Source=bd_coal.db;Version=3";
        SQLiteConnection sQLiteConnection = new SQLiteConnection(connect);
        DataTable dataTable = new DataTable();
        public OrderForm parentsForm;

        public AuthorizationForm()
        {
            InitializeComponent();
            //string path = Path.Combine(Directory.GetParent(Application.StartupPath).FullName, "Debug\\Database1.mdf");
            //connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=30";
            //SqlConnection = new SqlConnection(connect);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(telTextBox.Text) && !string.IsNullOrWhiteSpace(passwordTextBox.Text))//проверка на то что бы все данные были заполнен
            {
                sQLiteConnection.Open();
                string command = @"Select * From Клиенты
                                   where Номер_телефона='" + telTextBox.Text + "' and Пароль='" + passwordTextBox.Text + "'";

                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command, sQLiteConnection);
                dataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)//если не находит выдает сообщение
                {
                    MessageBox.Show("Данные введены не правильно!!!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sQLiteConnection.Close();
                }
                else
                {
                    this.Visible = false;
                    parentsForm.addres = dataTable.Rows[0]["Адрес"].ToString();
                    parentsForm.tel = dataTable.Rows[0]["Номер_телефона"].ToString();
                    parentsForm.Visible=true;
                    sQLiteConnection.Close();
                }
            }
            else
            {
                MessageBox.Show("Заполните все данные!!!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }       
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            this.Visible = false;
            registrationForm.Visible = true;
            registrationForm.formParent = this;
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

        private void AuthorizationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = false;
            parentsForm.Visible = true;
        }
    }
}
