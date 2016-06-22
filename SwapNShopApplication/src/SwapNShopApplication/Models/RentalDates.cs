using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class RentalDates
    {
        [Key]
        public int IdRentalDates { get; set; }
        public int IdEquipment { get; set; }
        public int IdMusician { get; set; }
        public DateTime checkOutDates { get; set; }
        public DateTime checkInDates { get; set; }
        public bool confirmed { get; set; }

        //public Equipment Equipment { get; set; }
        //public Musician Musician { get; set; }
    }
}
