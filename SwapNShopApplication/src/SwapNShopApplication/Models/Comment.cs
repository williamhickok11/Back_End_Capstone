using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class Comment
    {
        [Key]
        public int IdComment { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }

        //public ICollection<CommentList> CommentList { get; set; }
    }
}
