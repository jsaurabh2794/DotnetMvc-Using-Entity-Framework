using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MHMSystem.Models
{
    public class Bookings
    {
        [Key]
        public int bookingId { get; set; }

        public int userId { get; set; }

        public int hallId { get; set; }

        public string bookingHallname { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

    }
}
