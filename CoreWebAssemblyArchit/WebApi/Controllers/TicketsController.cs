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
    [ApiVersion("1.0")]
    //decorate the class as APIController (not an MVC controller)
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
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
        public async Task<IActionResult> Get() 
        {
            return Ok(await db.Tickets.ToListAsync());
            //400s user error, 500s server error
            //return Ok("Reading all the tix");
        }

        [HttpGet("{id}")]
        //[Route("api/tickets/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            //do a try catch at SaveChanges
            await db.SaveChangesAsync();

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
        public async Task<IActionResult> Put(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            db.Entry(ticket).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (await db.Tickets.FindAsync(id) == null)
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Route("api/tickets/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            //marked as deleted
            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync ();

            return Ok(ticket);
        }








    }
}
