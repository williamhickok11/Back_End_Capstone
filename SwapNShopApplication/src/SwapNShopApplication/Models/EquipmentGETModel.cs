using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class EquipmentGETModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double pricePerDay { get; set; }
        public string condition { get; set; }
        public string category { get; set; }
        public string musician { get; set; }
        public int musicianID { get; set; }
        public int rentalRequests { get; set; }
        public IQueryable<Picture> picList { get; set; }
    }
}