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

            Musician m = _context.Musician.Single(e => e.IdMusician == id);

            if (m == null)
            {
                return NotFound();
            }

            return Ok(m);
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