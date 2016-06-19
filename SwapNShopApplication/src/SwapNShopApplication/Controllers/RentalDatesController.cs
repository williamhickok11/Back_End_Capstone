using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwapNShopApplication.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SwapNShopApplication.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class RentalDatesController : Controller
    {

        private SwapNShopDbContext _context;

        public RentalDatesController(SwapNShopDbContext context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery]int? EquipmentID)
        {
            var rentalDates = from rd in _context.RentalDates
                              join m in _context.Musician
                              on rd.IdMusician equals m.IdMusician
                              select new
                              {
                                  IdRentalDates = rd.IdRentalDates,
                                  checkInDates = rd.checkInDates,
                                  checkOutDates = rd.checkOutDates,
                                  IdEquipment = rd.IdEquipment,
                                  musician = m.userName
                              };

            // Get all the rental dates for one piece of equipment
            if (EquipmentID != null)
            {
                rentalDates = rentalDates.Where(r => r.IdEquipment == EquipmentID);
            }

            return Ok(rentalDates);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {   //STEVES CODE
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RentalDates m = _context.RentalDates.Single(e => e.IdRentalDates == id);

            if (m == null)
            {
                return NotFound();
            }

            return Ok(m);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] RentalDates rentaldates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the new rental dates
            _context.RentalDates.Add(rentaldates);
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
