using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class NotificationList
    {
        [Key]
        public int IdNotificationList { get; set; }
        public int IdNotification { get; set; }
        public int IdMusician { get; set; }
    }
}
