using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Models.ModelViewDataTables
{
    public class PullDataForm
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Frequency")]
        public string Frequency { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public Int32 NO_Frequency { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Average values or Instant Values")]
        public bool ValueOption { get; set; }
    }
}
