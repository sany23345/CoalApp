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
    public partial class OrderForm : Form
    {
        public string addres="";
        public double distance;
        static  string connect = @"Data Source=DESKTOP-DJUDJM1\SQLEXPRESS;Initial Catalog=BD_Coal;Integrated Security=True";
        SqlConnection SqlConnection = new SqlConnection(connect);
        public string geoocod;
        DataTable dataTableProvider = new DataTable();
        DataTable dataTableStanp = new DataTable();

        public OrderForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MapForm mapForm = new MapForm();
            mapForm.ParentForm1 = this;
            mapForm.Visible = true;
        }

        private void OrderForm_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = addres;
                label7.Text = "Растояние: " + Math.Round(distance) + " км";
            }
            catch (Exception){ }        
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            string qure = "Select * from Поставщики";
            SqlDataAdapter dataAdapter= new SqlDataAdapter(qure, SqlConnection);
            dataAdapter.Fill(dataTableProvider);
            providerComboBox.DataSource = dataTableProvider;
            providerComboBox.DisplayMember = "Наименование";
            providerComboBox.ValueMember = "id";        
        }


        private void providerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (providerComboBox.ValueMember == "id")
            {
                dataTableStanp = new DataTable();
                string qure = @"Select  Поставляемый_уголь.id, Марки_угля.Наименование_марки, Цена_за_1_тонну from Поставляемый_уголь
                               inner join Марки_угля on Марки_угля.id=Поставляемый_уголь.id_Угля
                               where Поставляемый_уголь.id_Поставщика='" + providerComboBox.SelectedValue.ToString() + "'";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(qure, SqlConnection);
                dataAdapter.Fill(dataTableStanp);
                stampСoalСomboBox.DataSource = dataTableStanp;
                stampСoalСomboBox.DisplayMember = "Наименование_марки";
                stampСoalСomboBox.ValueMember = "id";

                geoocod = dataTableProvider.Rows[(int)providerComboBox.SelectedIndex]["Адрес_Координаты"].ToString();//передача георасположения разреза
            }
        }

        private void stampСoalСomboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label3.Text = "Стоимость 1т угля: " + dataTableStanp.Rows[stampСoalСomboBox.SelectedIndex]["Цена_за_1_тонну"].ToString() + " руб.";
        }
    }
}
