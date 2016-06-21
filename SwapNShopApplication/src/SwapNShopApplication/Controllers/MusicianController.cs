using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SwapNShopApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SwapNShopApplication.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class MusicianController : Controller
    {
        private SwapNShopDbContext _context;

        public MusicianController(SwapNShopDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Musician> users = from user in _context.Musician
                                         select new Musician
                                         {
                                             IdMusician = user.IdMusician,
                                             userName = user.userName,
                                             email = user.email,
                                             city = user.city,
                                             state = user.state,
                                             rating = user.rating,
                                             description = user.description
                                         };

            if (username != null)
            {
                users = users.Where(g => g.userName == username);
            }

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {   //STEVES CODE
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<object> selectedUser = from user in _context.Musician
                                              where user.IdMusician == id
                                              select new
                                              {
                                                  IdMusician = user.IdMusician,
                                                  userName = user.userName,
                                                  email = user.email,
                                                  city = user.city,
                                                  state = user.state,
                                                  rating = user.rating,
                                                  description = user.description,
                                                  comments = from cl in _context.CommentList
                                                             join c in _context.Comment
                                                             on cl.IdComment equals c.IdComment
                                                             select new Comment
                                                             {
                                                                 date = c.date,
                                                                 message = c.message
                                                             }
                                              };

            if (selectedUser == null)
            {
                return NotFound();
            }

            return Ok(selectedUser);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Musician musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = from g in _context.Musician
                               where g.userName == musician.userName
                               select g;

            if (existingUser.Count<Musician>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }


            _context.Musician.Add(musician);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MusicianExists(musician.IdMusician))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtRoute("GetMusician", new { id = musician.IdMusician }, musician);
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Musician updatedMusician)
        {
            // Build out the updatedMusician by the id passed in
            var musicianToSave = from m in _context.Musician
                                 where m.IdMusician == id
                                 select new Musician
                                 {
                                     IdMusician = m.IdMusician,
                                     rating = m.rating,
                                     description = updatedMusician.description,
                                     city = updatedMusician.city,
                                     state = updatedMusician.state,
                                     email = updatedMusician.email,
                                     userName = updatedMusician.userName
                                 };

            var newMusician = musicianToSave.First();
            _context.Musician.Attach(newMusician);
            _context.Entry(newMusician).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool MusicianExists(int id)
        {
            return _context.Musician.Count(e => e.IdMusician == id) > 0;
        }
    }
}





//// PUT api/values/5
//[HttpPut("{id}")]
//public IActionResult Get(int id, [FromBody]string value)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest(ModelState);
//    }

//    IQueryable<Musician> users = from user in _context.Musician
//                                 select new Musician
//                                 {
//                                     IdMusician = user.IdMusician,
//                                     userName = user.userName,
//                                     email = user.email,
//                                     description = user.description,
//                                     city = user.city,
//                                     state = user.state,
//                                     rating = user.rating,
//                                 };

//    if (username != null)
//    {
//        users = users.Where(g => g.Username == username);
//    }

//    if (users == null)
//    {
//        return NotFound();
//    }

//    return Ok(users);
//}