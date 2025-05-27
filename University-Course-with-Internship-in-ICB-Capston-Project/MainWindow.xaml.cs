using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.ServiceModel.Channels;
using System.Windows.Input;

namespace Course_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlDocument doc = new XmlDocument();
        private ObservableCollection<ReportSensor> rSensors = new ObservableCollection<ReportSensor>();

        public ObservableCollection<Sensor> sensors = new ObservableCollection<Sensor>();

        public ObservableCollection<Coordinates> coords = new ObservableCollection<Coordinates>();
        public Dictionary<string, double> list = new Dictionary<string, double>();
        private Range range = new Range();

        private string latitude = "00.0000";

        private string longitude = "00.0000";
        private string description = null;
        private string sensorId = "0";
        private string sensorType = "open";
        private DispatcherTimer dTimer;
        string[] splCoord;
        string findSensorId;
        string seType;


        public MainWindow()
        {
            InitializeComponent();



            sensors = DeserializeSensors();
            AllSensors.ItemsSource = null;
            AllSensors.ItemsSource = sensors;
            PushpinCoordinates();

        }
        #region      MethodsForAnotherClasses
        private void SerializeSensors(ObservableCollection<Sensor> sensors)
        {
            XmlSerialiser xmlSerialiser = new XmlSerialiser(sensors);
            xmlSerialiser.SensorstoXML();

        }


        private void PushpinCoordinates()
        {


            try
            {
                foreach (var s in sensors)

                {
                    for (int i = 0; i < s.Coordinates.Length; i++)
                    {
                        splCoord = s.Coordinates.Split(' ');

                        try
                        {
                            coords.Add(new Coordinates(double.Parse(splCoord[0]), double.Parse(splCoord[1]), s));
                        }
                        catch (System.FormatException) { }
                        catch (Exception) { }
                    }
                }
            }
            catch (System.NullReferenceException)
            {
                return;
            }


            PinToBothMaps();


        }
        private void NewPin(Sensor s)
        {
            if (latitude != "Latitude" && longitude != "Longitude")
            {
                if (latitude != null && longitude != null)
                {
                    coords.Add(new Coordinates() { Latitude = Convert.ToDouble(latitude), Longitude = Convert.ToDouble(longitude), Sensor = s });

                }
            }
            PinToBothMaps();
        }

        public void PinToBothMaps()
        {
            foreach (var cor in coords)
            {
                Color color;
                switch (cor.Sensor.SensorType)
                {
                    case "open":
                        color = Colors.Green;
                        break;
                    case "humidity":
                        color = Colors.Blue;
                        break;
                    case "noise":
                        color = Colors.Orange;
                        break;
                    case "temperature":
                        color = Colors.Red;
                        break;
                    case "power":
                        color = Colors.Black;
                        break;
                    default:
                        color = Colors.Transparent;
                        break;
                }
                int index = sensors.IndexOf(cor.Sensor);
                AddPin(cor.Latitude, cor.Longitude, color, index);
                AddbPin2(cor.Latitude, cor.Longitude, color, index);

            }
        }

        private ObservableCollection<Sensor> DeserializeSensors()
        {
            try
            {
                doc.Load(@"Sensors.xml");

                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                doc.WriteTo(tx);

                string str = sw.ToString();
                string xmlString = str;
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Sensor>), new XmlRootAttribute("Sensors"));
                StringReader stringReader = new StringReader(xmlString);
                sensors = (ObservableCollection<Sensor>)serializer.Deserialize(stringReader);
                return sensors;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        #endregion

        #region Methods and Wpf events
        void AddPin(double latitude, double longitude, Color color, int sensorIndex)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Background = new SolidColorBrush(color);
            pushpin.Cursor = Cursors.Hand;
            pushpin.MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                Pushpin_MouseDown(sensorIndex);
            };
            MapLayer.SetPosition(pushpin, new Location(latitude, longitude));
            MapXaml.Children.Add(pushpin);
        }

        void AddbPin2(double latitude, double longitude, Color color, int sensorIndex)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Background = new SolidColorBrush(color);
            pushpin.Cursor = Cursors.Hand;
            pushpin.MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                Pushpin_MouseDown(sensorIndex);
            };
            MapLayer.SetPosition(pushpin, new Location(latitude, longitude));
            mp.Children.Add(pushpin);
        }

        private void Pushpin_MouseDown(int sensorIndex)
        {
            AllSensors.SelectedIndex = sensorIndex;
            GetGraphycalSensor();
            Dispatcher.BeginInvoke((Action)(() => tControl.SelectedIndex = 3));
        }

        public void tControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                var item = sender as TabControl;
                var selected = item.SelectedItem as TabItem;

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (sensors == null)
            {
                sensors = new ObservableCollection<Sensor>();
            }

            sensorId = (sensors.Count + 1).ToString();

            double lat = Convert.ToDouble(latitude);
            double longi = Convert.ToDouble(longitude);
            double x = lat + 0.0011;
            double y = longi + 0.0022;
            longitude = y.ToString();
            latitude = x.ToString();
            if (Convert.ToInt32(sensorId) % 2 == 0)
            {
                description = "home";
            }
            else
            {
                description = "office";
            }
            sensors.Insert(0, new Sensor(sensorId, latitude + " " + longitude, description, range.ToString(), sensorType));

            AddSensorinXml();
            SerializeSensors(sensors);
            AllSensors.ItemsSource = sensors;
        }

        private void AddSensorinXml()
        {
            if (File.Exists("Sensors.xml"))
            {


                XElement xEle = XElement.Load("Sensors.xml");
                xEle.Add(new XElement("Sensor",
                     new XElement("SensorId", sensorId),
                     new XElement("Coordinates", latitude + " " + longitude),
                        new XElement("Description", description),
                        new XElement("Range", range),
                    new XElement("SensorType", sensorType)));
            }
            else
            {
                XmlSerialiser xmlSerialiser = new XmlSerialiser(sensors);
            }

        }

        private void AllSensors_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var currentS = AllSensors.SelectedItem as Sensor;
            int ind = AllSensors.SelectedIndex;
            foreach (var s in sensors)
            {

                string csCoordinates = currentS.Coordinates;
                string[] cssCoordinates = csCoordinates.Split(' ');
                latitude = cssCoordinates[0];
                longitude = cssCoordinates[1];

            }
            SerializeSensors(sensors);
            sensors = DeserializeSensors();
            AllSensors.ItemsSource = sensors;
            NewPin(currentS);

        }


        #endregion



        #region Buttons

        private void LoadRSensors_Click(object sender, RoutedEventArgs e)
        {
            if (rSensors != null)
            {
                rSensors.Clear();
            }
            try
            {
                foreach (var s in sensors)
                {
                    WebAPIClient wac = new WebAPIClient();
                    if (range.ChechRange(sensorType, wac.GetCurrentValue(s.SensorID, s.SensorType))) ;
                    {

                        if (wac.webApiValueStyle != null)

                            rSensors.Add(new ReportSensor(s.SensorID, s.Coordinates, s.Description, s.Range, s.SensorType, (wac.webApiValueStyle)));

                    }
                }
            }
            catch (System.NullReferenceException) { }
            AllReports.ItemsSource = rSensors;
            AllReports.Columns[0].Visibility = Visibility.Hidden;
            dTimer = new DispatcherTimer();
            dTimer.Tick += new EventHandler(OnValue1);
            dTimer.Interval = new TimeSpan(0, 0, 1);
            dTimer.Start();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AllSensors.SelectedItem != null)
            {
                try
                {

                    for (int i = 0; i < coords.Count; i++)
                    {
                        mp.Children.Clear();
                        MapXaml.Children.Clear();
                    }
                    var currentS = AllSensors.SelectedItem as Sensor;
                    int ind = AllSensors.SelectedIndex;
                    sensors.RemoveAt(ind);
                    SerializeSensors(sensors);
                    sensors = DeserializeSensors();
                    AllSensors.ItemsSource = sensors;
                }
                catch (System.IndexOutOfRangeException) { }
                catch (Exception) { }
                coords.Clear();
                PushpinCoordinates();


            }
        }
        private void RefrMap_Click(object sender, RoutedEventArgs e)
        {

            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {

            AllSensors.CancelEdit();
            PushpinCoordinates();
            SerializeSensors(sensors);
            sensors = DeserializeSensors();
            AllSensors.ItemsSource = sensors;
        }

        private void BtnLink_Click(object sender, RoutedEventArgs e)
        {
            GetGraphycalSensor();

        }

        private void GetGraphycalSensor()
        {
            stcHumidity.Visibility = Visibility.Hidden;
            rsHumidity.Visibility = Visibility.Hidden;
            var currentS = AllSensors.SelectedItem as Sensor;
            int ind = AllSensors.SelectedIndex;
            try
            {
                var cVal = WebAPIClient.GetSensorValue(sensors[ind].SensorID, sensors[ind].SensorType);
                graphRepresentation.IsSelected = true;
                if (sensors[ind].SensorType != "open")
                {
                    lblGraphRepresentation.Content = string.Format($"{Convert.ToDouble(cVal):F1}");
                    bdrLabel.Height = 50;
                    bdrLabel.Width = 100;
                    bdrLabel.BorderBrush = Brushes.Khaki;
                    bdrLabel.BorderThickness = new Thickness(2);
                }
                if (sensors[ind].SensorType == "humidity")
                {
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (sensors[ind].SensorType == "noise")
                {
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (sensors[ind].SensorType == "temperature")
                {
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (sensors[ind].SensorType == "power")
                {
                    bdrLight.Visibility = Visibility.Hidden;
                    bdrLight.Background = Brushes.Black;
                    stcLight.Visibility = Visibility.Hidden;
                    stcLbl.Background = Brushes.Black;
                    lblGraphRepresentation.Foreground = Brushes.White;
                    lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                }

                if (sensors[ind].SensorType == "open")
                {
                    stcHumidity.Visibility = Visibility.Hidden;

                    if (cVal == "true")
                    {
                        lblGraphRepresentation.Content = string.Format("Open");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Green;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Khaki;
                        GreenLight.Fill = Brushes.Green;
                    }
                    if (cVal == "false")
                    {
                        lblGraphRepresentation.Content = string.Format("Close");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Red;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Red;
                        GreenLight.Fill = Brushes.Khaki;
                    }
                    if (sensors[ind].SensorType == "power")
                    {
                        bdrLight.Visibility = Visibility.Hidden;
                        bdrLight.Background = Brushes.Black;
                        stcLight.Visibility = Visibility.Hidden;
                        stcLbl.Background = Brushes.Black;
                        lblGraphRepresentation.Foreground = Brushes.White;
                        lbl2GraphRepresentation.Visibility = Visibility.Hidden;
                    }
                }
                dTimer = new DispatcherTimer();
                dTimer.Tick += new EventHandler(OnValue);
                dTimer.Interval = new TimeSpan(0, 0, 1);
                dTimer.Start();
            }
            catch (System.ArgumentOutOfRangeException) { }
        }

        private void OnValue(object sender, EventArgs args)
        {

            bdrLight.Height = 0;
            bdrLight.Width = 0;
            string cVal;
            var currentS = AllSensors.SelectedItem as Sensor;
            int ind = AllSensors.SelectedIndex;
            try
            {
                cVal = WebAPIClient.GetSensorValue(sensors[ind].SensorID, sensors[ind].SensorType);

                if (sensors[ind].SensorType == "humidity")
                {
                    stcHumidity.Visibility = Visibility.Visible;
                    rsHumidity.Visibility = Visibility.Visible;
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (sensors[ind].SensorType == "noise")
                {
                    stcHumidity.Visibility = Visibility.Visible;
                    rsHumidity.Visibility = Visibility.Visible;
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (sensors[ind].SensorType == "temperature")
                {
                    stcHumidity.Visibility = Visibility.Visible;
                    rsHumidity.Visibility = Visibility.Visible;
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (sensors[ind].SensorType == "power")
                {
                    bdrLight.Visibility = Visibility.Hidden;
                    bdrLight.Background = Brushes.Black;
                    stcLight.Visibility = Visibility.Hidden;
                    stcLbl.Background = Brushes.Black;
                    lblGraphRepresentation.Foreground = Brushes.White;
                    lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                }
                if (sensors[ind].SensorType != "open")
                {
                    lblGraphRepresentation.Content = string.Format($"{Convert.ToDouble(cVal):F1}");
                }
                if (sensors[ind].SensorType == "open")
                {
                    if (cVal == "true")
                    {
                        stcHumidity.Visibility = Visibility.Hidden;

                        lblGraphRepresentation.Content = string.Format("Open");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Green;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Khaki;
                        GreenLight.Fill = Brushes.Green;
                    }
                    if (cVal == "false")
                    {
                        lblGraphRepresentation.Content = string.Format("Close");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Red;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Red;
                        GreenLight.Fill = Brushes.Khaki;
                    }


                }
            }
            catch (Exception) { }
        }
        private void OnValue1(object sender, EventArgs args)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);

            LoadRSensors.RaiseEvent(newEventArgs);
        }

        private void OnValueSearch(object sender, EventArgs args)
        {

            bdrLight.Height = 0;
            bdrLight.Width = 0;
            string cVal = null;
            try
            {
                cVal = WebAPIClient.GetSensorValue(findSensorId, seType);

                if (seType == "humidity")
                {
                    stcHumidity.Visibility = Visibility.Visible;
                    rsHumidity.Visibility = Visibility.Visible;
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (seType == "noise")
                {
                    stcHumidity.Visibility = Visibility.Visible;
                    rsHumidity.Visibility = Visibility.Visible;
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (seType == "temperature")
                {
                    stcHumidity.Visibility = Visibility.Visible;
                    rsHumidity.Visibility = Visibility.Visible;
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (seType == "power")
                {
                    lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                    bdrLight.Visibility = Visibility.Hidden;
                    bdrLight.Background = Brushes.Black;
                    stcLight.Visibility = Visibility.Hidden;
                    stcLbl.Background = Brushes.Black;
                    lblGraphRepresentation.Foreground = Brushes.White;
                }
                if (seType != "open")
                {
                    lblGraphRepresentation.Content = string.Format($"{Convert.ToDouble(cVal):F1}");
                }
                if (seType == "open")
                {
                    if (cVal == "true")
                    {
                        stcHumidity.Visibility = Visibility.Hidden;

                        lblGraphRepresentation.Content = string.Format("Open");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Green;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Khaki;
                        GreenLight.Fill = Brushes.Green;
                    }
                    if (cVal == "false")
                    {
                        lblGraphRepresentation.Content = string.Format("Close");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Red;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Red;
                        GreenLight.Fill = Brushes.Khaki;
                    }


                }

            }
            catch (Exception) { }


        }

        #endregion

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dTimer.Stop();
            }
            catch (Exception) { }
            findSensorId = txtBFindId.Text.ToString();
            seType = null;
            string fsd = null;
            foreach (var s in sensors)
            {

                if (findSensorId == s.SensorID)
                {
                    fsd = "2";
                }

            }
            if (fsd != "2")
            {
                MessageBox.Show("There isn't sensor with this ID!");
            }
            if (fsd == "2")
            {
                foreach (var s in sensors)
                {
                    if (s.SensorID == findSensorId)
                    {
                        seType = s.SensorType;
                    }
                }
                GetGraphycalSEnsorSearchingID(findSensorId, seType);

            }
            txtBFindId.Clear();

        }

        private void GetGraphycalSEnsorSearchingID(string findSensorId, string seType)
        {
            try
            {
                string cVal = WebAPIClient.GetSensorValue(findSensorId, seType);
                graphRepresentation.IsSelected = true;

                if (seType != "open")
                {
                    lblGraphRepresentation.Content = string.Format($"{Convert.ToDouble(cVal):F1}");
                    bdrLabel.Height = 50;
                    bdrLabel.Width = 100;
                    bdrLabel.BorderBrush = Brushes.Khaki;
                    bdrLabel.BorderThickness = new Thickness(2);
                }
                if (seType == "humidity")
                {
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (seType == "noise")
                {
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (seType == "temperature")
                {
                    lblGraphRepresentation.Visibility = Visibility.Hidden;
                    bdrLabel.Visibility = Visibility.Hidden;

                }
                if (seType == "power")
                {
                    lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                    bdrLight.Visibility = Visibility.Hidden;
                    bdrLight.Background = Brushes.Black;
                    stcLight.Visibility = Visibility.Hidden;
                    stcLbl.Background = Brushes.Black;
                    lblGraphRepresentation.Foreground = Brushes.White;
                    lblGraphRepresentation.FontSize = 26;
                }

                if (seType == "open")
                {
                    stcHumidity.Visibility = Visibility.Hidden;

                    if (cVal == "true")
                    {
                        lblGraphRepresentation.Content = string.Format("Open");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Green;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Khaki;
                        GreenLight.Fill = Brushes.Green;
                    }
                    if (cVal == "false")
                    {
                        lblGraphRepresentation.Content = string.Format("Close");
                        lbl2GraphRepresentation.Content = lblGraphRepresentation.Content;
                        lbl2GraphRepresentation.Foreground = Brushes.Red;
                        bdrLight.Height = 250;
                        bdrLight.Width = 130;
                        stcLight.Background = Brushes.Black;
                        bdrLight.BorderBrush = Brushes.Black;
                        bdrLight.BorderThickness = new Thickness(2);
                        RedLight.Fill = Brushes.Red;
                        GreenLight.Fill = Brushes.Khaki;
                    }
                }
                dTimer = new DispatcherTimer();
                dTimer.Tick += new EventHandler(OnValueSearch);
                dTimer.Interval = new TimeSpan(0, 0, 1);
                dTimer.Start();
            }
            catch (System.ArgumentOutOfRangeException) { }
        }
    }
}
