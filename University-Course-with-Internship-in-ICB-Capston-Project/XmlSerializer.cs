using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Course_Project
{
    /// <summary>
    /// <Property class name="XmlSerialiser"></Propertyclass></Property>
    /// </summary>
    public class XmlSerialiser
    {
        private readonly ObservableCollection<Sensor> sensors = new ObservableCollection<Sensor>();

        #region Constructors


        public XmlSerialiser(ObservableCollection<Sensor> sensors)
        {
            this.sensors = sensors;
        }

        #endregion

        #region Properties
        public ObservableCollection<ReportSensor> Sensor { get; set; }
        #endregion

        #region Methods
        public void SensorstoXML()
        {
            if (sensors != null)
            {
                try
                {
                    
                        XDocument sensorsXML = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                            new XElement("Sensors",
                         from s in sensors
                         select
                      new XElement("Sensor",
                      new XElement("SensorID", s.SensorID),
                      new XElement("Coordinates", s.Coordinates),
                         new XElement("Description", s.Description),
                         new XElement("Range", s.Range),
                     new XElement("SensorType", s.SensorType))));



                        sensorsXML.Save("Sensors.xml");
                    
                }
               catch (System.NullReferenceException)
                {
                 
                }

                catch (System.Exception)
                {
                }
                return;

            }
            #endregion
        }
    }
}
