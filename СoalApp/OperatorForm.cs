using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СoalApp
{
  
    public partial class OperatorForm : Form
    {
        public OrderForm orderForm;
        static string connect = "Data Source=bd_coal.db;Version=3";
        SQLiteConnection sQLiteConnection = new SQLiteConnection(connect);
        public OperatorForm()
        {
            InitializeComponent();
            //string path = Path.Combine(Directory.GetParent(Application.StartupPath).FullName, "Debug\\Database1.mdf");
            //connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=30";
            //SqlConnection = new SqlConnection(connect);
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
                               ORDER BY Дата_заказа";
            SQLiteDataAdapter dataAdapter  = new SQLiteDataAdapter(zapros, sQLiteConnection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
