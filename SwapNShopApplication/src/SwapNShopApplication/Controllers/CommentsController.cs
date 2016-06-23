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
    public class CommentsController : Controller
    {
        private SwapNShopDbContext _context;

        public CommentsController(SwapNShopDbContext context)
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
            var comments = from c in _context.Comment
                           join cl in _context.CommentList
                           on c.IdComment equals cl.IdComment

                           join m in _context.Musician
                           on c.IdMusician equals m.IdMusician

                           where cl.IdMusician == id
                           select new
                           {
                               message = c.message,
                               date = c.date,
                               sender = m.userName,
                               senderId = m.IdMusician
                           };
            return Ok(comments);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CommentPost comm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the equipment
            Comment postingComment = new Comment
            {
                date = comm.date,
                IdMusician = comm.IdPostingMusician,
                message = comm.message
            };
            _context.Comment.Add(postingComment);
            _context.SaveChanges();

            Comment newComment = (from c in _context.Comment
                                 select new Comment
                                 {
                                     IdComment = c.IdComment,
                                 }).Last();

            CommentList commList = new CommentList
            {
                IdComment = newComment.IdComment,
                IdMusician = comm.IdRecievingMusician
            };
            _context.CommentList.Add(commList);
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
