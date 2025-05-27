
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace Course_Project
{
    /// <summary>
    /// <Property class name="WebAPIClient"></Propertyclass></Property>
    /// </summary>
    public class WebAPIClient
    {
        public static string sensorId = "sensorId";
        public float currentValue;
        private string webApiValue;
        private string webApiValueShort = null;
        public string webApiValueStyle = null;
        public ObservableCollection<ReportSensor> rSensors = new ObservableCollection<ReportSensor>();

        public ObservableCollection<Sensor> sensors = new ObservableCollection<Sensor>();

        public ObservableCollection<Coordinates> coords = new ObservableCollection<Coordinates>();
        private Range range = new Range();

        private ReportSensor sens = new ReportSensor();
        public enum Sensor_Type { power, noise, humidity, open, temperature }
        Sensor_Type sensType = new Sensor_Type();


        #region Methods

        public static string GetSensorValue(string sensorId, string sensorType)
        {

            var client = new HttpClient
            {
                BaseAddress = new Uri("http://smartdormitory.azurewebsites.net/api/sensorvalues")
            };
            client.DefaultRequestHeaders.Add("Authorization", "B56BZ7FD7UEL8JKDZRCD7P5TMW2VVFLD");
            try
            {
                var response = client.GetAsync($"?sensorType={(Sensor_Type)Enum.Parse(typeof(Sensor_Type), sensorType, true)}&sensorId={sensorId}").Result;



                return response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : null;
            }
            catch (System.AggregateException e)
            {
                MessageBox.Show("Problem is:" + e);
                return null;
            }
            catch (Exception ) { return null; }
        }


        public string GetCurrentValue(string sensrId, string sensorType)
        {
            string currenttValue = null;
            switch (sensorType.ToString())
            {
                case "power":
                    currenttValue = GetSensorPowerCurrentValue(sensorId, sensorType);
                    break;
                case "humidity":
                    currenttValue = GetSensorHumidityCurrentValue(sensorId, sensorType);
                    break;
                case "open":
                    currenttValue = GetSensorOpenCurrentValue(sensorId, sensorType);
                    break;
                case "temperature":
                    currenttValue = GetSensorTemperatureCurrentValue(sensorId, sensorType);
                    break;
                case "noise":
                    currenttValue = GetSensorNoiseCurrentValue(sensorId, sensorType);
                    break;


            }
            return currenttValue;
        }

        public string GetSensorPowerCurrentValue(string sensorId, string sensorType)
        {

            try
            {
                webApiValue = WebAPIClient.GetSensorValue(WebAPIClient.sensorId.ToString(), Sensor_Type.power.ToString());


                char[] ch = new char[4];
                webApiValue.CopyTo(0, ch, 0, ch.Length);

                webApiValueShort = ch[0].ToString() + ch[1] + ch[2] + ch[3];

                if (range.ChechRange(sensorType, webApiValueShort))
                {

                    rSensors.Add(new ReportSensor(sensorType, webApiValueStyle));

                    switch (sensType)
                    {

                        case Sensor_Type.power:

                            webApiValueStyle = webApiValueShort + " W.";
                            break;

                    }
                }
                return webApiValueShort;
            }
            catch (System.NullReferenceException) { return null; }


        }

        public string GetSensorHumidityCurrentValue(string sensorId, string sensorType)
        {
            try { 

            webApiValue = WebAPIClient.GetSensorValue(WebAPIClient.sensorId.ToString(), Sensor_Type.humidity.ToString());



            char[] ch = new char[4];
            webApiValue.CopyTo(0, ch, 0, ch.Length);

            webApiValueShort = ch[0].ToString() + ch[1] + ch[2] + ch[3];

            if (range.ChechRange(sensorType, webApiValueShort))
            {

                rSensors.Add(new ReportSensor(sensorType, webApiValueStyle));

                

                        webApiValueStyle = webApiValueShort + " %.";
                        
            }
            return webApiValueShort;
            }
            catch (System.NullReferenceException) { return null; }

        }


        public string GetSensorTemperatureCurrentValue(string sensorId, string sensorType)
        {

            try { 
            webApiValue = WebAPIClient.GetSensorValue(WebAPIClient.sensorId.ToString(), Sensor_Type.temperature.ToString());



            char[] ch = new char[4];
            webApiValue.CopyTo(0, ch, 0, ch.Length);

            webApiValueShort = ch[0].ToString() + ch[1] + ch[2] + ch[3];

            if (range.ChechRange(sensorType, webApiValueShort))
            {

                rSensors.Add(new ReportSensor(sensorType, webApiValueStyle));

                

                        webApiValueStyle = webApiValueShort + " °C.";
                        
            }
            return webApiValueShort;
            }
            catch (System.NullReferenceException) { return null; }

        }
        public string GetSensorOpenCurrentValue(string sensorId, string sensorType)
        {


            try { 
            webApiValue = WebAPIClient.GetSensorValue(WebAPIClient.sensorId.ToString(), Sensor_Type.open.ToString());



            char[] ch = new char[4];
            webApiValue.CopyTo(0, ch, 0, ch.Length);

            webApiValueShort = ch[0].ToString() + ch[1] + ch[2] + ch[3];

            if (range.ChechRange(sensorType, webApiValueShort))
            {

                rSensors.Add(new ReportSensor(sensorType, webApiValueStyle));

                        webApiValueStyle = "Open door or window .";
                
               
            }
            return webApiValueShort;
            }
            catch (System.NullReferenceException) { return null; }

        }
        public string GetSensorNoiseCurrentValue(string sensorId, string sType)
        {
            try { 
            webApiValue = WebAPIClient.GetSensorValue(WebAPIClient.sensorId.ToString(), Sensor_Type.noise.ToString());


            char[] ch = new char[4];
            webApiValue.CopyTo(0, ch, 0, ch.Length);

            webApiValueShort = ch[0].ToString() + ch[1] + ch[2] + ch[3];

            if (range.ChechRange(sType, webApiValueShort))
            {

                rSensors.Add(new ReportSensor(sType, webApiValueStyle));


                        webApiValueStyle = webApiValueShort + " dB.";
                 
            }
            return webApiValueShort;
            }
            catch (System.NullReferenceException) { return null; }

        }
    }
    #endregion

}

  


    



