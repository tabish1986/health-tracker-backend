using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{   
    public class BloodPressureSummary
    {
        public string LastReading { get; set; }
        public string MaxReading { get; set;}
        public string MinReading { get; set;}
        public string graphSYS { get; set; }
        public string graphDYS { get; set; }
    }

    public class BloodPressureReadings
    {
        public string ID { get; set; }
        public string SNO { get; set; }
        public string READING_DATE { get; set; }
        public string READING_TIME { get; set; }
        public string SYS_READING { get; set; }
        public string DYS_READING { get; set; }
        public string DEVICE { get; set; }
        public string LASTMEAL { get; set; }
        public string LASTMEALTIME { get; set; }
    }

    public class BloodPressureReadingsGraphStats
    {
        public string minValue { get; set; }
        public string maxValue { get; set; }
        public List<string> XAxis { get; set; }
        public List<string> YAxis_SYS { get; set; }
        public List<string> YAXIS_DYS { get; set; }        
    }
}
