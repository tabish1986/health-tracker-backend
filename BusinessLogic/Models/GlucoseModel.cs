using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class GlucoseReadings
    {
        public string ID { get; set; }
        public string SNO { get; set; }
        public string READING_DATE { get; set; }
        public string READING_TIME { get; set; }
        public string READING { get; set; }
        public string TYPE { get; set; }
        public string DEVICE { get; set; }
        public string LASTMEAL { get; set; }
        public string LASTMEALTIME { get; set; }
    }

    public class GlucoseReadingsGraphStats
    {
        public string minValue { get; set; }
        public string maxValue { get; set; }
        public List<string> XAxis { get; set; }
        public List<string> YAxis_FASTING { get; set; }
        public List<string> YAXIS_RANDOM { get; set; }
    }
}
