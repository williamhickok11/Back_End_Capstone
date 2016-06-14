using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        //public ICollection<Equipment> Equipment { get; set; }
    }
}
