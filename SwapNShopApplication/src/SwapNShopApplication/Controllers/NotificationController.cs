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
                                       sender = m.userName,
                                       newRentalRequest = n.newRentalRequest,
                                       ID = n.IdNotification
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
                newRentalRequest = nfs.newRentalRequest
            };
            _context.Notification.Add(notification);
            _context.SaveChanges();

            // Get access to the notification you just posted
            Notification newNotification = (from n in _context.Notification
                                            select new Notification
                                            {
                                                IdNotification = n.IdNotification,
                                                IdMusician = n.IdMusician,
                                                description = n.description,
                                                newRentalRequest = n.newRentalRequest
                                            }).Last();
            //Check to see if this is a new rental request for the front end to handle
            //if (nfs.description.Contains("requested"))
            //{
            //    newNotification.newRentalRequest = true;

            //} else
            //{
            //    newNotification.newRentalRequest = false;
            //}
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
        [HttpDelete("{id}/{rentalRequest}")]
        public IActionResult Delete(int id, bool rentalRequest)
        {
            // Delete the notification based on the rentalRequest and reciever id
            if (rentalRequest == true)
            {
                var rentalRequestNote = from n in _context.Notification
                                        where n.IdMusician == id && n.newRentalRequest == true
                                        select new Notification
                                        {
                                            IdMusician = n.IdMusician,
                                            IdNotification = n.IdNotification,
                                            description = n.description,
                                            newRentalRequest = n.newRentalRequest
                                        };
                var RRnote = rentalRequestNote.First();

                var noteList = from nl in _context.NotificationList
                               where nl.IdNotification == RRnote.IdNotification
                               select new NotificationList
                               {
                                   IdMusician = nl.IdMusician,
                                   IdNotification = nl.IdNotification,
                                   IdNotificationList = nl.IdNotificationList
                               };
                var RRnoteList = noteList.First();

                _context.NotificationList.Remove(RRnoteList);
                _context.SaveChanges();
                _context.Notification.Remove(RRnote);
                _context.SaveChanges();
            } else
            {
                // Delete the notelist based off the note id
                var nList = from nl in _context.NotificationList
                            where nl.IdNotification == id
                            select new NotificationList
                            {
                                IdNotificationList = nl.IdNotificationList,
                                IdMusician = nl.IdMusician,
                                IdNotification = nl.IdNotification
                            };
                var noteList = nList.First();
                _context.NotificationList.Remove(noteList);
                _context.SaveChanges();

                // Delete the note
                var notification = from n in _context.Notification
                                   where n.IdNotification == id
                                   select new Notification
                                   {
                                       IdNotification = n.IdNotification,
                                       IdMusician = n.IdMusician,
                                       description = n.description,
                                       newRentalRequest = n.newRentalRequest
                                   };
                var note = notification.First();
                _context.Notification.Remove(note);
                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
