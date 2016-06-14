using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class Musician
    {
        [Key]
        public int IdMusician { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public int rating { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string description { get; set; }

        //public ICollection<CommentList> CommentList { get; set; }
        //public ICollection<Equipment> Equipment { get; set; }
        //public ICollection<RentalDates> RentalDates { get; set; }
    }
}
