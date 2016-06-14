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
    public class InventoryController : Controller
    {
        private SwapNShopDbContext _context;

        public InventoryController(SwapNShopDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> equipment = from e in _context.Equipment
                                           join m in _context.Musician
                                           on e.IdMusician equals m.IdMusician

                                           join c in _context.Category
                                           on e.IdCategory equals c.IdCategory

                                           select new
                                           {
                                               name = e.name,
                                               description = e.description,
                                               pricePerDay = e.pricePerDay,
                                               condition = e.condition,
                                               category = c.title,
                                               musician = m.userName,
                                               PicListHref = String.Format("/api/Pictures?equipmentId={0}", e.IdEquipment)
                                           };

            return Ok(equipment);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IQueryable<object> equipment = from e in _context.Equipment
                                           join m in _context.Musician
                                           on e.IdMusician equals m.IdMusician

                                           join c in _context.Category
                                           on e.IdCategory equals c.IdCategory

                                           where e.IdEquipment == id
                                           select new
                                           {
                                               name = e.name,
                                               description = e.description,
                                               pricePerDay = e.pricePerDay,
                                               condition = e.condition,
                                               category = c.title,
                                               musician = m.userName,
                                               PicListHref = String.Format("/api/Pictures?equipmentId={0}", e.IdEquipment),
                                               RentalDates = from e in _context.Equipment
                                                             join rd in _context.RentalDates
                                                             on e.IdEquipment equals rd.IdEquipment

                                                             join m in _context.Musician
                                                             on rd.IdMusician equals m.IdMusician
                                                             select new
                                                             {
                                                                 musician = m.userName,
                                                                 dateIN = rd.checkOutDates,
                                                                 dateOUT = rd.checkInDates
                                                             }
                                           };

            return Ok(equipment);
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
