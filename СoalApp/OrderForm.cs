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
        public string addres = "";
        public double distance;
        static string connect = @"Data Source=DESKTOP-DJUDJM1\SQLEXPRESS;Initial Catalog=BD_Coal;Integrated Security=True";
        SqlConnection SqlConnection = new SqlConnection(connect);
        public string geoocod;
        DataTable dataTableProvider = new DataTable();
        DataTable dataTableStanp = new DataTable();
        double shippingCost, fullSumma;

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
                addressTextBox.Text = addres;
                label7.Text = "Растояние: " + Math.Round(distance) + " км";

                //от 1т до 3 - 10 р за километр
                //от 4т до 7т 15 р за километр
                //от 8т до 20т 35 р за километр
                //от 21 до 40т 80 р.за километр

                double costDeliver = 0;
                double costCoal = 0;

                if (stampСoalСomboBox.SelectedItem != null)
                {
                    costCoal = double.Parse(dataTableStanp.Rows[stampСoalСomboBox.SelectedIndex]["Цена_за_1_тонну"].ToString());
                }

                int quantity = (int)numericUpDown1.Value;
                distance = Math.Round(distance);
                if (quantity >= 1 && quantity <= 3)
                {
                    costDeliver = 10;
                }
                else if (quantity >= 4 && quantity <= 7)
                {
                    costDeliver = 15;
                }
                else if (quantity >= 8 && quantity <= 20)
                {
                    costDeliver = 35;
                }
                else if (quantity >= 21 && quantity < 40)
                {
                    costDeliver = 80;
                }

                shippingCost = costDeliver * distance;

                label9.Text = "Стоимость доставки:" + shippingCost + "руб.";

                //полную стоимость
                //масса угля *цену сорта угля +цена за километр *на километраж

                fullSumma = quantity * costCoal + shippingCost;//рачет полной стоимости заказа

                label6.Text = "Общая стоимость заказа: " + fullSumma + "руб";
            }
            catch (Exception) { }
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            string qure = "Select * from Поставщики";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(qure, SqlConnection);
            dataAdapter.Fill(dataTableProvider);
            providerComboBox.DataSource = dataTableProvider;
            providerComboBox.DisplayMember = "Наименование";
            providerComboBox.ValueMember = "id";

            button1.Enabled = false;
        }

        private void providerComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            button1.Enabled = true;
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
        private void stampСoalСomboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            label3.Text = "Стоимость 1т угля: " + dataTableStanp.Rows[stampСoalСomboBox.SelectedIndex]["Цена_за_1_тонну"].ToString() + " руб.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0 && !string.IsNullOrEmpty(providerComboBox.Text)
                && !string.IsNullOrEmpty(stampСoalСomboBox.Text) && !string.IsNullOrEmpty(addressTextBox.Text))
            {

                this.Visible = false;

                ConfirmationForm confirmationForm = new ConfirmationForm();

                confirmationForm.parentForm = this;

                confirmationForm.provider = providerComboBox.Text;
                confirmationForm.stamp = stampСoalСomboBox.Text;
                confirmationForm.pricePerTon = dataTableStanp.Rows[stampСoalСomboBox.SelectedIndex]["Цена_за_1_тонну"].ToString();
                confirmationForm.requiredWeight = numericUpDown1.Value.ToString();
                confirmationForm.address = addressTextBox.Text;
                confirmationForm.distance = distance.ToString();
                confirmationForm.shippingCost = shippingCost.ToString();
                confirmationForm.fullPrice = fullSumma.ToString();
                confirmationForm.Visible = true;
            }
            else
            {
                MessageBox.Show("Укажите все необходимые данные!!!");
            }
        }
    }
}
