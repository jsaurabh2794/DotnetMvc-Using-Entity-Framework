using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MHMSystem.Models
{
    public class MarriageHall
    {
        [Key]
        public int hallId { get; set; }

        public string hallName { get; set; }

        public string hallAddress { get; set; }
    }
}
