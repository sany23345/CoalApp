using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Net;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.IO;

namespace СoalApp
{
    public partial class MapForm : Form
    {
        List<PointLatLng> points;//лист координат
        GMapOverlay mapOverlay = new GMapOverlay("markers");//создание массива маркеров
        PointLatLng factoryCoalPoints;//переменная для сохранения точки разреза\
        public double distance;//переменная для сохранения дистанции от меткипользователя до метки разреза
        GeoCoordinate user, coalMine;
        string myAPI = @"AIzaSyBTahSEYrJIElOfmD7bTcSlKDjz9bbFsAM";
        public OrderForm ParentForm1;
        public RegistrationForm regPrentForm;
        string finalAddress;
        string[] mas = new string[2];
        bool point = true;
        public string markForm;

        public MapForm()
        {
            InitializeComponent();
            points = new List<PointLatLng>();
        }

        public void CostDistance()
        {
            distance = user.GetDistanceTo(coalMine);
            distance = Math.Ceiling(distance);
            distance = Math.Round(distance / 1000);
            ParentForm1.distance = distance;
            label2.Text = "Растояние:" + distance.ToString() + "км";
        }

        private void MapForm_Load(object sender, EventArgs e)//исправить проблему при регистраци
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;//какая карта 
            GMap.NET.GMaps.Instance.Mode = AccessMode.ServerOnly;

            gMapControl1.DragButton = MouseButtons.Left;//на какую кнопку осуществляется перемещение по карте
            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            gMapControl1.ShowCenter = false;
            gMapControl1.RoutesEnabled = true;

            gMapControl1.Position = new PointLatLng(53.717383971381906, 91.42661132907293);//начальные координаты при загрузки
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 20;
            gMapControl1.Zoom = 10;
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            if (markForm == "заказ")
            {
                string[] mas = ParentForm1.geoocod.Split(',');

                for (int i = 0; i < mas.Length; i++)
                {
                    mas[i] = mas[i].Replace('.', ',');
                }

                factoryCoalPoints = new PointLatLng(Convert.ToDouble(mas[0].ToString()), Convert.ToDouble(mas[1].ToString()));//сохранение координат разреза
                coalMine = new GeoCoordinate(Convert.ToDouble(mas[0].ToString()), Convert.ToDouble(mas[1].ToString()));

                //установка маркера разреза
                PointLatLng point = new PointLatLng(factoryCoalPoints.Lat, factoryCoalPoints.Lng);//создание координат
                GMapMarker gMapMarker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);//создание маркера
                gMapMarker.ToolTipText = "Разрез";
                mapOverlay.Markers.Add(gMapMarker);//добавление маркера в масиив           
                gMapControl1.Overlays.Add(mapOverlay);//добавление массива маркеоров на карту
            }
            else
            {
                label2.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)//подтверждение адреса
        {
            if (markForm == "заказ")
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    ParentForm1.addres = finalAddress;

                    if (GMapOverlayAddressDoubleClick.Markers.Count != 0)
                    {
                        user = new GeoCoordinate(GMapOverlayAddressDoubleClick.Markers[0].Position.Lat, GMapOverlayAddressDoubleClick.Markers[0].Position.Lng);
                    }
                    else if (GMapOverlayAddress.Markers.Count != 0)
                    {
                        user = new GeoCoordinate(GMapOverlayAddress.Markers[0].Position.Lat, GMapOverlayAddress.Markers[0].Position.Lng);
                    }

                    ParentForm1.Visible = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Укажите адрес!!!");
                }
            }
            else if (markForm == "регистрация")
            {
                regPrentForm.regAddress = finalAddress;
                regPrentForm.Visible = true;
                this.Close();
            }
        }

        GMapOverlay GMapOverlayAddressDoubleClick = new GMapOverlay("AddressDoubleClick");

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (point == false)
            {
                MessageBox.Show("Метка уже установлена!!!");
            }
            else
            {
                double x = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
                double y = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

                string zapros = "https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=true or false&language=ru&key=" + myAPI;

                string url = string.Format(zapros, x.ToString().Replace(',', '.'), y.ToString().Replace(',', '.'));

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(dataStream);
                string responseReader = streamReader.ReadToEnd();//все данные которые получили с запроса
                response.Close();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseReader);

                if (xmlDocument.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
                {
                    finalAddress = "";
                    string formatAddress = xmlDocument.SelectNodes("//formatted_address").Item(0).InnerText.ToString();

                    textBox1.Text = formatAddress;
                    finalAddress = formatAddress;

                    string[] words = formatAddress.Split(',');
                    string dataMarker = string.Empty;
                    foreach (string word in words)
                    {
                        dataMarker += word + ";" + Environment.NewLine;
                    }

                    gMapControl1.Overlays.Add(GMapOverlayAddressDoubleClick);
                    GMarkerGoogle addresMarker = new GMarkerGoogle(new PointLatLng(x, y), GMarkerGoogleType.red_dot);
                    addresMarker.ToolTip = new GMapRoundedToolTip(addresMarker);
                    addresMarker.ToolTipMode = MarkerTooltipMode.Always;
                    addresMarker.ToolTipText = dataMarker;
                    GMapOverlayAddressDoubleClick.Markers.Add(addresMarker);
                }

                if (GMapOverlayAddressDoubleClick.Markers.Count != 0)
                {
                    user = new GeoCoordinate(GMapOverlayAddressDoubleClick.Markers[0].Position.Lat, GMapOverlayAddressDoubleClick.Markers[0].Position.Lng);
                }
                else if (GMapOverlayAddress.Markers.Count != 0)
                {
                    user = new GeoCoordinate(GMapOverlayAddress.Markers[0].Position.Lat, GMapOverlayAddress.Markers[0].Position.Lng);
                }

                point = false;

                if (markForm=="заказ")
                {
                    CostDistance();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//очистка маркеров
        {
            points.Clear();
            gMapControl1.Overlays.Clear();
            mapOverlay.Clear();
            GMapOverlayAddress.Clear();
            GMapOverlayAddressDoubleClick.Clear();

            //установка маркера разреза        
            PointLatLng pointMarker = new PointLatLng(factoryCoalPoints.Lat, factoryCoalPoints.Lng);//создание координат
            GMapMarker gMapMarker = new GMarkerGoogle(pointMarker, GMarkerGoogleType.red_dot);//создание маркера
            gMapMarker.ToolTipText = "Разрез";
            mapOverlay.Markers.Add(gMapMarker);//добавление маркера в масиив           
            gMapControl1.Overlays.Add(mapOverlay);//добавление массива маркеоров на карту
            point = true;
        }

        private void button2_Click(object sender, EventArgs e)//автоопределение кординат
        {
            if (point == false)
            {
                MessageBox.Show("Метка уже установлена!!!");
            }
            else
            {
                bool abort = false;//пременная для ошибки
                GeoCoordinate coordinate;
                GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);

                if (watcher.TryStart(false, TimeSpan.FromMilliseconds(3000)))
                {
                    DateTime start = DateTime.Now;
                    while (watcher.Status != GeoPositionStatus.Ready && !abort)
                    {
                        Thread.Sleep(200);
                        if (DateTime.Now.Subtract(start).TotalSeconds > 5)
                        {
                            abort = true;
                        }
                    }

                    coordinate = watcher.Position.Location;

                    if (!coordinate.IsUnknown)
                    {
                        double Lat = coordinate.Latitude;
                        double Lon = coordinate.Longitude;

                        string zapros = "https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=true or false&language=ru&key=" + myAPI;

                        string url = string.Format(zapros, Lat.ToString().Replace(',', '.'), Lon.ToString().Replace(',', '.'));

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        WebResponse response = request.GetResponse();
                        Stream dataStream = response.GetResponseStream();
                        StreamReader streamReader = new StreamReader(dataStream);
                        string responseReader = streamReader.ReadToEnd();//все данные которые получили с запроса
                        response.Close();

                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(responseReader);

                        if (xmlDocument.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
                        {
                            finalAddress = "";

                            string formatAddress = xmlDocument.SelectNodes("//formatted_address").Item(0).InnerText.ToString();
                            textBox1.Text = formatAddress;
                            finalAddress = formatAddress;

                            string[] words = formatAddress.Split(',');
                            string dataMarker = string.Empty;
                            foreach (string word in words)
                            {
                                dataMarker += word + ";" + Environment.NewLine;
                            }

                            gMapControl1.Overlays.Add(GMapOverlayAddressDoubleClick);
                            GMarkerGoogle addresMarker = new GMarkerGoogle(new PointLatLng(Lat, Lon), GMarkerGoogleType.red_dot);
                            addresMarker.ToolTip = new GMapRoundedToolTip(addresMarker);
                            addresMarker.ToolTipMode = MarkerTooltipMode.Always;
                            addresMarker.ToolTipText = dataMarker;
                            GMapOverlayAddressDoubleClick.Markers.Add(addresMarker);

                            gMapControl1.Position = new PointLatLng(Lat, Lon);
                        }

                        if (GMapOverlayAddressDoubleClick.Markers.Count != 0)
                        {
                            user = new GeoCoordinate(GMapOverlayAddressDoubleClick.Markers[0].Position.Lat, GMapOverlayAddressDoubleClick.Markers[0].Position.Lng);
                        }
                        else if (GMapOverlayAddress.Markers.Count != 0)
                        {
                            user = new GeoCoordinate(GMapOverlayAddress.Markers[0].Position.Lat, GMapOverlayAddress.Markers[0].Position.Lng);
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Координаты не определены");
                    }
                }

                point = false;

                if (markForm == "заказ")
                {
                    CostDistance();
                }
            }
        }

        GMapOverlay GMapOverlayAddress = new GMapOverlay("address");

        private void button4_Click(object sender, EventArgs e)//уставновка метки по адресу
        {
            if (point == false)
            {
                MessageBox.Show("Метка уже установлена!!!");
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    finalAddress = "";

                    string zapros = "https://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true or false&language=ru&key=" + myAPI;

                    string url = string.Format(zapros, Uri.EscapeDataString(textBox1.Text));

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(dataStream);
                    string responseReader = streamReader.ReadToEnd();//все данные которые получили с запроса
                    response.Close();

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(responseReader);

                    if (xmlDocument.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
                    {
                        XmlNodeList nodes = xmlDocument.SelectNodes("//location");

                        double latitude = 0.0;
                        double longitude = 0.0;

                        foreach (XmlNode node in nodes)
                        {
                            latitude = XmlConvert.ToDouble(node.SelectSingleNode("lat").InnerText.ToString());
                            longitude = XmlConvert.ToDouble(node.SelectSingleNode("lng").InnerText.ToString());
                        }

                        string formatAddress = xmlDocument.SelectNodes("//formatted_address").Item(0).InnerText.ToString();

                        textBox1.Text = formatAddress;
                        finalAddress = formatAddress;

                        string[] words = formatAddress.Split(',');
                        string dataMarker = string.Empty;
                        foreach (string word in words)
                        {
                            dataMarker += word + ";" + Environment.NewLine;
                        }

                        gMapControl1.Overlays.Add(GMapOverlayAddress);
                        GMarkerGoogle addresMarker = new GMarkerGoogle(new PointLatLng(latitude, longitude), GMarkerGoogleType.red_dot);
                        addresMarker.ToolTip = new GMapRoundedToolTip(addresMarker);
                        addresMarker.ToolTipMode = MarkerTooltipMode.Always;
                        addresMarker.ToolTipText = dataMarker;
                        GMapOverlayAddress.Markers.Add(addresMarker);

                        gMapControl1.Position = new PointLatLng(latitude, longitude);
                    }

                    if (GMapOverlayAddressDoubleClick.Markers.Count != 0)
                    {
                        user = new GeoCoordinate(GMapOverlayAddressDoubleClick.Markers[0].Position.Lat, GMapOverlayAddressDoubleClick.Markers[0].Position.Lng);
                    }
                    else if (GMapOverlayAddress.Markers.Count != 0)
                    {
                        user = new GeoCoordinate(GMapOverlayAddress.Markers[0].Position.Lat, GMapOverlayAddress.Markers[0].Position.Lng);
                    }
                }
                else
                {
                    MessageBox.Show("Напишите адресс!!!");
                }
                point = false;

                if (markForm == "заказ")
                {
                    CostDistance();
                }
            }
        }

        private void MapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (markForm == "заказ")
            {
                ParentForm1.Visible = true;
            }
            else
            {
                regPrentForm.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)//расчет дистанции
        {
            //distance = user.GetDistanceTo(coalMine);
            //distance = Math.Ceiling(distance);
            //distance = Math.Round(distance / 1000);
            //ParentForm1.distance = distance;
            //label2.Text = "Растояние:" + distance.ToString() + "км";
        }
    }
}
