using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHMSystem.Models
{
    public class ApplicationDbContextForHallBookingDates : DbContext
    {
        public ApplicationDbContextForHallBookingDates(DbContextOptions<ApplicationDbContextForHallBookingDates> options) : base(options)
        {

        }
        public DbSet<HallBookingDates> HallBookingDates { get; set; }
    }
}
