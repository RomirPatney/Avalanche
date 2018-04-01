using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models.Tables
{
    public class Settings
    {
        [Key]
        public Int32 ID { get; set; }

        public string Frequency { get; set; }

        public Int32 NO_Frequency { get; set; }

        public bool ValueOption { get; set; }

        public Settings(string frequency, int nO_Frequency, bool valueOption)
        {
            Frequency = frequency ?? throw new ArgumentNullException(nameof(frequency));
            NO_Frequency = nO_Frequency;
            ValueOption = valueOption;
        }
    }
}
