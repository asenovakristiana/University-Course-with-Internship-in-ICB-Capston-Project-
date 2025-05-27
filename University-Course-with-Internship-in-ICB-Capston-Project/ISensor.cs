using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Project
{
    /// <summary>
    /// <Property interface name="Coordinates"></Propertyclass></Property>
    /// </summary>
    public interface ISensor
    {

        string SensorID { get; set; }
         string Coordinates { get; set; }
        
         string Description { get; set; }

        Range Range { get; set; }
       
       
    }
}
