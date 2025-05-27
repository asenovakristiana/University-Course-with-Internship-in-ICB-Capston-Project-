using System.ComponentModel;
using System.Windows;

namespace Course_Project
{
    /// <summary>
    /// <Property class name="Coordinates"></Propertyclass></Property>
    /// </summary>
    public class Coordinates : DependencyObject, INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double latitude;
        private double longitude;
        private string coordinates;
        private Sensor sensor;




        #region Constructors
        public Coordinates()
        {
            latitude = 43.2046f;
            longitude = 27.9105f;

        }
        public Coordinates(double latitude, double longitude, Sensor sensor)
        {
            Latitude = latitude;
            Longitude = longitude;
            Sensor = sensor;
        }
        public Coordinates(string coordinates)
        {
            this.coordinates = coordinates;
            Latitude = latitude;
            Longitude = longitude;
        }



        #endregion

        #region Properties
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                if (this.latitude != value)
                {
                    this.latitude = value;
                    this.NotifyPropertyChanged("latitude");
                }
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                if (this.longitude != value)
                {
                    this.longitude = value;
                    this.NotifyPropertyChanged("longitude");
                }
            }
        }
        public string SCoordinates
        {
            get
            {
                return coordinates;
            }
            set
            {
                if (this.coordinates != value)
                {
                    this.coordinates = Latitude.ToString() + " " + Longitude.ToString();
                    this.NotifyPropertyChanged("coordinates");
                }
            }
        }

        public Sensor Sensor
        {
            get
            {
                return sensor;
            }
            set
            {
                if (this.sensor != value)
                {
                    this.sensor = value;
                    this.NotifyPropertyChanged("sensor");
                }
            }
        }


        #endregion

        #region NotifyPropertyChanged
        public void NotifyPropertyChanged(string pChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pChanged));
        }
        #endregion



    }

}




