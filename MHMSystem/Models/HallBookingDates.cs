using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MHMSystem.Models
{
    public class HallBookingDates
    {
        [Key]
        public int id { get; set; }
        public int hallId { get; set; }
        public string hallName { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
}
