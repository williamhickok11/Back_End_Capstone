using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class Picture
    {
        [Key]
        public int IdPicture { get; set; }
        public string image { get; set; }

        //public ICollection<PictureList> PictureList { get; set; }
    }
}
