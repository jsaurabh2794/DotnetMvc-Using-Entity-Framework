using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MHMSystem.Models
{
    public class ApplicationDbContextForMarriageHall : DbContext
    {
        public ApplicationDbContextForMarriageHall(DbContextOptions<ApplicationDbContextForMarriageHall> dbContextOptionsForMarriageHall) : base(dbContextOptionsForMarriageHall)
        {

        }
        public DbSet<MarriageHall> MarriageHall { get; set; }
    }
}
