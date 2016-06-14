using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class CommentList
    {
        [Key]
        public int IdCommentList { get; set; }
        public int IdComment { get; set; }
        public int IdMusician { get; set; }

        //public Comment Comment { get; set; }
        //public Musician Musician { get; set; }
    }
}
