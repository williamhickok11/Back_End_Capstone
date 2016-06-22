using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class CommentPost
    {
        public int IdComment { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public int IdPostingMusician { get; set; }
        public int IdRecievingMusician { get; set; }
    }
}
