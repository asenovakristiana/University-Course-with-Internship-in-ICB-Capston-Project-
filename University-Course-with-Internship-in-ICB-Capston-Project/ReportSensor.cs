using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Course_Project
{
    /// <summary>
    /// <Property class name="ReportSensor"></Propertyclass></Property>
    /// </summary>
    public class ReportSensor : Sensor, INotifyPropertyChanged
    {

        string sensorType;
        readonly Coordinates coord = new Coordinates();
        readonly Coordinates latitude = new Coordinates();
        readonly Coordinates longitude = new Coordinates();

        #region Constructors

        public ReportSensor()
        {


        }
        public ReportSensor(string sensorId, string coordinates, string description, string range, string sensorType,string currentValue):base(sensorId, coordinates,description, range, sensorType)
        {
            this.currentValue = currentValue;

        }

        public ReportSensor(string sensorType, string currentValue) : this(currentValue)
        {
            this.sensorType = sensorType;
        }


        public ReportSensor(string currentValue) : this()
        {
            this.currentValue = currentValue;

        }




        #endregion






    }
}
