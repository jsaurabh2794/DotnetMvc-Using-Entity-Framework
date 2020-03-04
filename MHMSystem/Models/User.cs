using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MHMSystem.Models
{
    public class User
    {
        [Key]
        
        public int userId { get; set; }
        
        [Required]
        public string firstName { get; set; }
        
        public string lastName { get; set; }
        
        public string email { get; set; }
        
        public Int64 mobile { get; set; }

        public Int16 MarriageHallId { get; set; }


    }
}
