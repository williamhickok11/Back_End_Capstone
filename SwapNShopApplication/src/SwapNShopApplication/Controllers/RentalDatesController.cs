using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwapNShopApplication.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Get([FromQuery]int? EquipmentID, [FromQuery]int? MusicianID)
        {
            // Get all the rental dates for one piece of equipment
            if (EquipmentID != null)
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
                                      IdMusician = rd.IdMusician,
                                      musician = m.userName,
                                      confirmed = rd.confirmed
                                  };

                rentalDates = rentalDates.Where(r => r.IdEquipment == EquipmentID);
                return Ok(rentalDates);
            }
            if (MusicianID != null)
            {
                var rentalDates = from rd in _context.RentalDates
                                  join e in _context.Equipment
                                  on rd.IdEquipment equals e.IdEquipment

                                  join m in _context.Musician
                                  on e.IdMusician equals m.IdMusician
                                  where m.IdMusician == MusicianID
                                  select new
                                  {
                                      IdRentalDates = rd.IdRentalDates,
                                      checkInDates = rd.checkInDates,
                                      checkOutDates = rd.checkOutDates,
                                      equipmentName = e.name,
                                      confirmed = rd.confirmed,
                                      renter = (from mu in _context.Musician
                                                where mu.IdMusician == rd.IdMusician
                                                select new Musician
                                                {
                                                    IdMusician = mu.IdMusician,
                                                    userName = mu.userName,
                                                    city = mu.city,
                                                    state = mu.state
                                                })
                                  };

                return Ok(rentalDates);
            }
            return Ok();

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
        public IActionResult Post([FromBody] RentalDates rentalDates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the new rental dates
            _context.RentalDates.Add(rentalDates);
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id)//, [FromBody]string value
        {
            var rentalDates = from rd in _context.RentalDates
                              where rd.IdRentalDates == id
                              select rd;
            var rentdate = rentalDates.First();
            rentdate.confirmed = true;

            _context.RentalDates.Attach(rentdate);
            _context.Entry(rentdate).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rentalDates = from rd in _context.RentalDates
                              where rd.IdRentalDates == id
                              select rd;

            _context.RentalDates.Remove(rentalDates.First());
            _context.SaveChanges();

            return Ok();
        }
    }
}
