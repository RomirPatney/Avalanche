using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models.Tables
{
    public class Log
    {
        public DateTime Date { get; set; }
        public double HeartBeat { get; set; }
        public double Tremble { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public bool IsEmergency { get; set; }

        [Key]
        public Int64 ID { get; set; }

        public Log(DateTime date, double heartBeat, double tremble, double temperature, double humidity, bool isEmergency, long iD)
        {
            Date = date;
            HeartBeat = heartBeat;
            Tremble = tremble;
            Temperature = temperature;
            Humidity = humidity;
            IsEmergency = isEmergency;
            ID = iD;
        }

        public Log()
        {

        }
    }
}
