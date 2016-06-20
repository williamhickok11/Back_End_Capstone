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
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]NotificationCreation nfs, string requestType)
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
            Notification notification = new Notification();
            notification.IdPostingMusician = nfs.IdPostingMusician;
            if (requestType == "deny")
            {
                notification.description = "Your rental request has been denied";
            }
            else if (requestType == "accept")
            {
                notification.description = "Your rental request has been accepted";
            }
            else if (requestType == "request")
            {
                notification.description = "Your equipment has been requested to be rented";
            }

            // Get access to the notification you just posted
            Notification newNotification = (from n in _context.Notification
                                            select new Notification
                                            {
                                                IdNotification = n.IdNotification,
                                                IdPostingMusician = n.IdPostingMusician,
                                                description = n.description
                                            }).Last();
            // Post the link between the notification and the reciepient
            NotificationList nfsList = new NotificationList
            {
                IdRecievingMusician = nfs.IdRecievingMusician,
                IdNotification = newNotification.IdNotification,
            };
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
