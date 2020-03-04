using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MHMSystem.Models
{
    public class ApplicationDbContextForBookings : DbContext
    {
        public ApplicationDbContextForBookings(DbContextOptions<ApplicationDbContextForBookings> dbContextOptionsForBookings) : base(dbContextOptionsForBookings)
        {

        }
        public DbSet<Bookings> bookings { get; set; }
    }
}
