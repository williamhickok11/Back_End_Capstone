using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class Equipment
    {
        [Key]
        public int IdEquipment { get; set; }
        public int IdMusician { get; set; }
        public int IdCategory { get; set; }
        public string name { get; set; }
        public double pricePerDay { get; set; }
        public string condition { get; set; }
        public string description { get; set; }

        //public ICollection<PictureList> PictureList { get; set; }
        public ICollection<RentalDates> RentalDates { get; set; }

        //public Musician Musician { get; set; }
        //public Category Category { get; set; }
    }
}
