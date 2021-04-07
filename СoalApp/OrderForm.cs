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
using System.Device.Location;
using System.Net;
using System.IO;
using System.Xml;
using GMap.NET;

namespace СoalApp
{
    public partial class OrderForm : Form
    {
        public string addres, geoocod, tel;
        public double distance;
        double shippingCost, fullSumma;
        string[] mas;
        static string connect = @"Data Source=DESKTOP-DJUDJM1\SQLEXPRESS;Initial Catalog=BD_Coal;Integrated Security=True";
        string myAPI = @"AIzaSyBTahSEYrJIElOfmD7bTcSlKDjz9bbFsAM";
        SqlConnection SqlConnection = new SqlConnection(connect);
        DataTable dataTableProvider = new DataTable();
        DataTable dataTableStanp = new DataTable();
        GeoCoordinate user, coalMine;

        public OrderForm()
        {
            InitializeComponent();
        }

        public void FullSumma()//метод для рачета полной стоимости заказа
        {
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

        public void GetCoordinate()//Метод для получения координат и расчета дистанции
        {
            try
            {
                if (!string.IsNullOrEmpty(addressTextBox.Text) && providerComboBox.ValueMember == "id")
                {
                    string zapros = "https://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true or false&language=ru&key=" + myAPI;
                    string url = string.Format(zapros, Uri.EscapeDataString(addressTextBox.Text));

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(dataStream);
                    string responseReader = streamReader.ReadToEnd();//все данные которые получили с запроса
                    response.Close();

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(responseReader);

                    double latitude = 0.0;
                    double longitude = 0.0;

                    if (xmlDocument.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
                    {
                        XmlNodeList nodes = xmlDocument.SelectNodes("//location");//получение расположения 
                        foreach (XmlNode node in nodes)
                        {
                            latitude = XmlConvert.ToDouble(node.SelectSingleNode("lat").InnerText.ToString());//получение широты
                            longitude = XmlConvert.ToDouble(node.SelectSingleNode("lng").InnerText.ToString());//получение долготы
                        }
                    }

                    mas = geoocod.Split(',');//разделение строки на массив двух строк
                    for (int i = 0; i < mas.Length; i++)
                    {
                        mas[i] = mas[i].Replace('.', ',');//замена в этих строках . на ,
                    }

                    PointLatLng factoryCoalPoints = new PointLatLng(Convert.ToDouble(mas[0].ToString()), Convert.ToDouble(mas[1].ToString()));
                    user = new GeoCoordinate(latitude, longitude);
                    coalMine = new GeoCoordinate(factoryCoalPoints.Lat, factoryCoalPoints.Lng);

                    distance = user.GetDistanceTo(coalMine);
                    distance = Math.Ceiling(distance);
                    distance = distance / 1000;
                }
            }
            catch (Exception) { }
        }

        private void button1_Click(object sender, EventArgs e)//событие для перхода на форму карты 
        {
            this.Visible = false;
            MapForm mapForm = new MapForm();
            mapForm.ParentForm1 = this;
            mapForm.markForm = "заказ";
            mapForm.Visible = true;
        }

        private void OrderForm_VisibleChanged(object sender, EventArgs e)
        {
            geoocod = dataTableProvider.Rows[(int)providerComboBox.SelectedIndex]["Адрес_Координаты"].ToString();//сохранение георасположения разреза
            addressTextBox.Text = addres;

            GetCoordinate();
            FullSumma();
        }

        private void OrderForm_Load(object sender, EventArgs e)//событие при загрузке формы
        {
            string qure = "Select * from Поставщики";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(qure, SqlConnection);
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
                //строка запрос для получения марки угля
                string qure = @"Select  Поставляемый_уголь.id, Марки_угля.Наименование_марки, Цена_за_1_тонну from Поставляемый_уголь
                               inner join Марки_угля on Марки_угля.id=Поставляемый_уголь.id_Угля
                               where Поставляемый_уголь.id_Поставщика='" + providerComboBox.SelectedValue.ToString() + "'";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(qure, SqlConnection);
                dataAdapter.Fill(dataTableStanp);

                //заполнение данными СomboBox для марки угля
                stampСoalСomboBox.DataSource = dataTableStanp;
                stampСoalСomboBox.DisplayMember = "Наименование_марки";
                stampСoalСomboBox.ValueMember = "id";

                geoocod = dataTableProvider.Rows[(int)providerComboBox.SelectedIndex]["Адрес_Координаты"].ToString();//сохранение георасположения разреза
            }

            GetCoordinate();//Метод для получения координат и расчета дистанции
            FullSumma();//метод для рачета полной суммы заказа
        }
        private void stampСoalСomboBox_SelectedValueChanged(object sender, EventArgs e)//событие при выборе марки угля
        {
            if (stampСoalСomboBox.SelectedIndex != -1)
            {
                label3.Text = "Стоимость 1т угля: " + dataTableStanp.Rows[stampСoalСomboBox.SelectedIndex]["Цена_за_1_тонну"].ToString() + " руб.";
                FullSumma();//метод для рачета полной суммы заказа
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//событие при переходе на форму авторизации
        {
            //отрытие формы регистрации
            AuthorizationForm authorizationForm = new AuthorizationForm();
            authorizationForm.Visible = true;
            authorizationForm.parentsForm = this;
            this.Visible = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//событие при выборе количества тонн угля
        {
            FullSumma();//метод для рачета полной суммы заказа
        }

        private void button2_Click(object sender, EventArgs e)//событие при нажатии на кнопку заказать
        {
            //прверка на то что бы все поля были заполнены
            if (numericUpDown1.Value != 0 && !string.IsNullOrEmpty(providerComboBox.Text)
                && !string.IsNullOrEmpty(stampСoalСomboBox.Text) && !string.IsNullOrEmpty(addressTextBox.Text))
            {
                //отрытие формы подтвержения заказа
                ConfirmationForm confirmationForm = new ConfirmationForm();
                confirmationForm.parentForm = this;
                this.Visible = false;

                //перенос данных на дугую форму
                confirmationForm.provider = providerComboBox.Text;
                confirmationForm.stamp = stampСoalСomboBox.Text;
                confirmationForm.pricePerTon = dataTableStanp.Rows[stampСoalСomboBox.SelectedIndex]["Цена_за_1_тонну"].ToString();
                confirmationForm.requiredWeight = numericUpDown1.Value.ToString();
                confirmationForm.address = addressTextBox.Text;
                confirmationForm.distance = distance.ToString();
                confirmationForm.shippingCost = shippingCost.ToString();
                confirmationForm.fullPrice = fullSumma.ToString();
                confirmationForm.tel = tel;
                confirmationForm.Visible = true;
            }
            else
            {
                MessageBox.Show("Укажите все необходимые данные!!!");
            }
        }
    }
}
