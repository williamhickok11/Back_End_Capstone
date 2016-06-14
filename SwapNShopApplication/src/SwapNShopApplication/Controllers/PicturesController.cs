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
    public class PicturesController : Controller
    {
        private SwapNShopDbContext _context;

        public PicturesController(SwapNShopDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IQueryable<Picture> pictureList = from pl in _context.PictureList
                                              join e in _context.Equipment
                                              on pl.IdEquipment equals e.IdEquipment

                                              join p in _context.Picture
                                              on pl.IdPicture equals p.IdPicture

                                              where e.IdEquipment == id
                                              select new Picture
                                              {
                                                  image = p.image
                                              };
                          

            return Ok(pictureList);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
