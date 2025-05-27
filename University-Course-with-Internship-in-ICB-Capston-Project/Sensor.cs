using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Course_Project.ReportSensor;

namespace Course_Project
{
    /// <summary>
    /// <Property class name="Sensor"></Propertyclass></Property>
    /// </summary>
    public class Sensor : INotifyPropertyChanged, IDataErrorInfo
    {

        private string sensorType;

        private string coordinates;
        private string description;
        private string sensorId;
        private string range;
        public string currentValue;
        private string[] splCoord;
        private string[] splRange;
        readonly Coordinates coord = new Coordinates();
        readonly Coordinates latitude = new Coordinates();
        readonly Coordinates longitude = new Coordinates();
        public event PropertyChangedEventHandler PropertyChanged;
        public static string result = null;


        #region Constructors
        public Sensor()
        {
            coordinates = "00.0000 00.0000";
            range = "0-1";
            description = null;
            sensorId = "0";
            sensorType = "open";
        }

        public Sensor(string sensorId, string coordinates, string description, string range, string currentValue, string sensorType) : this(currentValue, sensorType, sensorId)
        {
            this.description = description;
            this.sensorId = sensorId;
            this.coordinates = coordinates;
            this.range = range;
            this.sensorType = sensorType;
            this.currentValue = currentValue;

        }

        public Sensor(string sensorId, string coordinates, string description, string range, string sensorType)
        {

            Description = description;
            SensorID = sensorId;
            Coordinates = coordinates;
            Range = range;
            SensorType = sensorType;


        }
        public Sensor(string currentValue, string sensor_Type, string sensorId) : this(currentValue, sensor_Type)
        {
            this.sensorId = sensorId;
        }

        public Sensor(string currentValue, string sensor_Type) : this(currentValue)
        {
            this.currentValue = currentValue;
            SensorType = sensor_Type;
        }


        public Sensor(string currentValue)
        {
            this.currentValue = currentValue;
            SensorType = sensorType;
        }






        #endregion

        #region Properties

        public string Error
        {
            get
            {
                return this[string.Empty];
            }
        }
        public string this[string correctString]
        {
            get
            {

                if (correctString == "Coordinates")
                {
                    if (string.IsNullOrEmpty(Coordinates))
                    {
                        result = "Coordinates cannot be empty!";

                        return result;
                    }


                    splCoord = Coordinates.Split(' ');

                    double resultt;
                    if (!double.TryParse(splCoord[0], out resultt) && !double.TryParse(splCoord[1], out resultt))
                    {
                        result = "Coordinates must be in latitude and longitude format!";
                        return result;
                    }
                }


                if (correctString == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        result = "Description cannot be empty!";
                        return result;
                    }

                }

                if (correctString == "SensorType")
                {
                    if (string.IsNullOrEmpty(SensorType))
                    {
                        result = "Sensor type cannot be empty!";
                        return result;
                    }

                    if (SensorType != "humidity" && SensorType != "noise" && SensorType != "open" && SensorType != "power" && SensorType != "temperature")
                    {
                        result = "Sensor type must be:humidity,power,open,noise or temperature!";
                        return result;
                    }
                }
                if (correctString == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        result = "Description cannot be empty!";
                        return result;
                    }

                }

                if (correctString == "Range")
                {
                    if (string.IsNullOrEmpty(Range))
                    {
                        result = "Range cannot be empty";
                        return result;
                    }

                    try
                    {
                        splRange = Range.Split('-');
                        int distance;

                        if (int.TryParse(splRange[0], out distance) == false || int.TryParse(splRange[1], out distance) == false)
                        {
                            result = "Range must be two numbers for min and max value,separated with " + "\"-\"" + "!";
                            return result;

                        }
                        if (Convert.ToInt32(splRange[0]) < -50 && Convert.ToInt32(splRange[0]) > 10000 && Convert.ToDouble(splRange[1]) < -50 && Convert.ToInt32(splRange[1]) > 10000)
                        {
                            result = "Range must be two numbers for min and max value,separated with " + "\"-\"" + "!";
                            return result;
                        }
                    }
                    catch (Exception) { result = "Range must be two numbers for min and max value,separated with " + "\"-\"" + "!"; return result; }
                }


                return null;

            }
        }
        public string CurrentValue
        {
            get { return currentValue; }
            set
            {
                if (this.currentValue != value)
                {
                    this.currentValue = value;
                    this.NotifyPropertyChanged("CurrentValue");
                }
            }
        }
        public string SensorID
        {
            get { return sensorId; }
            set
            {
                if (this.sensorId != value)
                {
                    this.sensorId = value;
                    this.NotifyPropertyChanged("SensorID");
                }
            }
        }
        public string Coordinates
        {
            get { return coordinates; }
            set
            {
                if (this.coordinates != value)
                {
                    this.coordinates = value;
                    this.NotifyPropertyChanged("Coordinates");

                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }

        }
        public string SensorType
        {
            get { return sensorType; }
            set
            {
                if (this.sensorType != value)
                {
                    this.sensorType = value;
                    this.NotifyPropertyChanged("SensorType");
                }
            }
        }

        public string Range
        {
            get { return range; }
            set
            {
                if (this.range != value)
                {
                    this.range = value;
                    this.NotifyPropertyChanged("Range");
                }
            }

        }




        #endregion

        #region Notify Property Changed

        public void NotifyPropertyChanged(string pChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pChanged));
        }


        #endregion


    }

}
