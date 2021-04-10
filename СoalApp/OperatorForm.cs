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
  
    public partial class OperatorForm : Form
    {
        public OrderForm orderForm;
        static string connect = @"Data Source=DESKTOP-DJUDJM1\SQLEXPRESS;Initial Catalog=BD_Coal;Integrated Security=True";
        SqlConnection SqlConnection = new SqlConnection(connect);
        public OperatorForm()
        {
            InitializeComponent();
        }

        private void OperatorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            orderForm.Visible = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OperatorForm_Load(object sender, EventArgs e)
        {
            string zapros = @"select Заказы.id as [Номер заказа], Номер_телефона_клиента,  Дата_заказа , Общая_цена,Подтверждение from Заказы
                               where Подтверждение=0
                               ORDER BY Дата_заказа";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(zapros,SqlConnection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
