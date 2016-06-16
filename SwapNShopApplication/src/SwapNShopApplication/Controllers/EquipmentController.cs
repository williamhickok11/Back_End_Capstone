using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SwapNShopApplication.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SwapNShopApplication.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class EquipmentController : Controller
    {
        private SwapNShopDbContext _context;

        public EquipmentController(SwapNShopDbContext context)
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
                                               picList = from pl in _context.PictureList
                                                         join eq in _context.Equipment
                                                         on pl.IdEquipment equals eq.IdEquipment

                                                         join p in _context.Picture
                                                         on pl.IdPicture equals p.IdPicture

                                                         where eq.IdEquipment == e.IdEquipment
                                                         select new
                                                         {
                                                             image = p.image
                                                         }
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
        public IActionResult Post([FromBody] EquipmentJSONModel equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Local list of Pics to use in the many to many relationship with the equipment
            List<int> picIDsForPicListTable = new List<int>();
            // Get access to a list of the images you have passed in
            var pictureList = equipment.images;
            // Loop though those images to add them to the database
            foreach (var pic in pictureList)
            {
                // create a new picture to add to the database
                Picture picture = new Picture();
                picture.image = pic.ToString();
                // Save to the database
                _context.Picture.Add(picture);
                _context.SaveChanges();
                // Get access to the id of the new pic and add to a list
                var picId = from p in _context.Picture
                            where p.image == pic
                            select new
                            {
                                ID = p.IdPicture
                            };
                picIDsForPicListTable.Add(picId.First().ID);   
            }

            // Add the equipment
            Equipment currEquipment = new Equipment
            {
                name = equipment.name,
                description = equipment.description,
                IdCategory = equipment.IdCategory,
                IdMusician = equipment.IdMusician,
                pricePerDay = equipment.pricePerDay,
                condition = equipment.condition
            };
            _context.Equipment.Add(currEquipment);
            _context.SaveChanges();

            // Get access to the equipment and grab the ID
            var equipId = from e in _context.Equipment
                          where e.description == currEquipment.description
                          && e.condition == currEquipment.condition
                          && e.name == currEquipment.name
                          select new
                          {
                              ID = e.IdEquipment
                          };
            
            // Add to PictureList with reference to the picIds and equipment id
            foreach (var item in picIDsForPicListTable)
            {
                PictureList pl = new PictureList
                {
                    IdEquipment = equipId.First().ID,
                    IdPicture = item
                };
            }

            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (GeekExists(geek.GeekId))
            //    {
            //        return new StatusCodeResult(StatusCodes.Status409Conflict);
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
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
