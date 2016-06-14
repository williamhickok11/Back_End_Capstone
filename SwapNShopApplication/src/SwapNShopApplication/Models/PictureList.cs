using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class PictureList
    {
        [Key]
        public int IdPictureList { get; set; }
        public int IdPicture { get; set; }
        public int IdEquipment { get; set; }

        //public Equipment Equipment { get; set; }
        //public Picture Picture { get; set; }
    }
}
