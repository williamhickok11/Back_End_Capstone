using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SwapNShopApplication.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SwapNShopApplication.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class NotificationController : Controller
    {
        private SwapNShopDbContext _context;

        public NotificationController(SwapNShopDbContext context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allNotifications = from n in _context.Notification
                                   join np in _context.NotificationList
                                   on n.IdNotification equals np.IdNotification

                                   join m in _context.Musician
                                   on n.IdMusician equals m.IdMusician
                                   where np.IdMusician == id
                                   select new
                                   {
                                       description = n.description,
                                       sender = m.userName
                                   };
            return Ok(allNotifications);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]NotificationCreation nfs)
        {
            //Get a reference to the user who has posted a request or
            var posterUserName = from m in _context.Musician
                                 where m.IdMusician == nfs.IdPostingMusician
                                 select new
                                 {
                                     name = m.userName
                                 };
            var recieverUserName = from m in _context.Musician
                                   where m.IdMusician == nfs.IdRecievingMusician
                                   select new
                                   {
                                       name = m.userName
                                   };

            //Post the notification
            Notification notification = new Notification
            {
                description = nfs.description,
                IdMusician = nfs.IdPostingMusician,
            };
            _context.Notification.Add(notification);
            _context.SaveChanges();

            // Get access to the notification you just posted
            Notification newNotification = (from n in _context.Notification
                                            select new Notification
                                            {
                                                IdNotification = n.IdNotification,
                                                IdMusician = n.IdMusician,
                                                description = n.description
                                            }).Last();
            // Post the link between the notification and the reciepient
            NotificationList nfsList = new NotificationList
            {
                IdMusician = nfs.IdRecievingMusician,
                IdNotification = newNotification.IdNotification,
            };
            _context.NotificationList.Add(nfsList);
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
