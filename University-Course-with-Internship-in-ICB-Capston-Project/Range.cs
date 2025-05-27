using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Course_Project
{
    /// <summary>
    /// <Property class name="Range"></Propertyclass></Property>
    /// </summary>
    public class Range : INotifyPropertyChanged
    {
        private int minRange;
        private int maxRange;
        public ObservableCollection<Sensor> eventSensors = new ObservableCollection<Sensor>();
        public string sensorType;
        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructors
        public Range()
        {
            minRange = 0;
            maxRange = 1;
        }
        public Range(int minRange, int maxRange)
        {
            MaxRange = maxRange;
            MinRange = minRange;
        }

        #endregion

        #region Properties
        public int MaxRange
        {
            get { return maxRange; }
            set
            {
                this.maxRange = value;
                this.NotifyPropertyChanged("maxRange");
            }
        }

        public int MinRange
        {
            get { return minRange; }
            set
            {
                this.minRange = value;
                this.NotifyPropertyChanged("minRange");
            }
        }




        #endregion

        #region NotifyPropertyChanged

        public void NotifyPropertyChanged(string pChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pChanged));
        }

        #endregion

        #region Methods

        public bool ChechRange(string sType, string cValue)
        {


            switch (sType)
            {

                case "temperature":
                    minRange = 15;
                    maxRange = 20;
                    try
                    {
                        if (float.Parse(cValue) < minRange || float.Parse(cValue) > maxRange)
                        {
                            if (sType == "temperature")
                            {
                                return true;
                            }
                        }
                    }
                    catch(System.ArgumentNullException) { }
                    break;
                case "open":
                    if (sType== "open")
                    {

                        if (cValue =="open" || cValue== "true" )
                        {
                            maxRange = 1;
                            return true;
                        }
                        break;

                    }
                    else
                    {
                        maxRange = 0;
                        return false;
                    }
                case "noise":
                    minRange = 1;
                    maxRange = 20;
                    if (sType == "noise")

                        if (float.Parse(cValue) < minRange || float.Parse(cValue) > maxRange)
                        {
                            return true;
                        }
                    break;

                case "power":
                    minRange = 1;
                    maxRange = 1000;
                    if (sType == "power")

                        if (float.Parse(cValue) < minRange || float.Parse(cValue) > maxRange)
                        {
                            return true;
                        }
                    break;
                case "humidity":
                    minRange = 45;
                    maxRange = 60;
                    if (sType == "humidity")

                        if (float.Parse(cValue) < minRange || float.Parse(cValue) > maxRange)
                        {
                            return true;
                        }
                    break;
                default:
                    return false;


            }
            return false;
        }

        public override string ToString()
        {

            return string.Format($"{this.minRange}-{this.maxRange}");
        }

        #endregion

    }
}








