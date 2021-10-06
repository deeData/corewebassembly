using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.Controllers
{
    //decorate the class as APIController (not an MVC controller)
    [ApiController]
    [Route("api/[controller]")]
    //[Version1DiscontinueResourceFilter] moved to global filter in Startup.cs
    public class TicketsController : ControllerBase //everything you need for a web API controller
    {
        private readonly BugsContext db;

        public TicketsController(BugsContext db)
        {
            this.db = db;
        }



        [HttpGet]
        //attribute routing
        //[Route("api/tickets")]
        //IActionResult returns all types- is generic
        public IActionResult Get() 
        {
            return Ok(db.Tickets.ToList());
            //400s user error, 500s server error
            //return Ok("Reading all the tix");
        }

        [HttpGet("{id}")]
        //[Route("api/tickets/{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            //do a try catch at SaveChanges
            db.SaveChanges();

            return CreatedAtAction(nameof(GetById),
                    new { id = ticket.TicketId },
                    ticket
                );
        }

        //[HttpPost]
        ////[Route("api/tickets")]
        //public IActionResult PostV1([FromBody] Ticket ticket)
        //{
        //    //Ok automatically serializes the obj into json
        //    return Ok(ticket);
        //}


        //[HttpPost]
        ////**********should probably be /api/V2/tickets
        ////v2 uses action filters so that change won't apply to v1
        //[Route("V2/")]
        ////add action filter attribute
        ////[Ticket_EnsureEnteredDate]
        //public IActionResult PostV2([FromBody] Ticket ticket)
        //{
        //    //Ok automatically serializes the obj into json
        //    return Ok(ticket);
        //}


        [HttpPut("{id}")]
        //[Route("api/tickets")]
        public IActionResult Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch
            {
                if (db.Tickets.Find(id) == null)
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Route("api/tickets/{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null) return NotFound();

            db.Tickets.Remove(ticket);
            db.SaveChanges();

            return Ok(ticket);
        }








    }
}
