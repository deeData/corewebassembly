using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.OData;
using WebApi.Filters.V2;
using WebApi.QueryFilters;

namespace CoreWebApp.Controllers.V2
{
    /// <summary>
    /// API change- Description becomes a required field- implement in [action] filter pipeline
    /// </summary>

    //version can be pulled by query string e.g. "https://localhost:44349/api/tickets?api-version=2.0"
    [ApiVersion("2.0")]
    [ApiController]
    //ApiVersion attribute above will replace v: in Url
    [Route("api/tickets")]
    public class TicketsV2Controller : ControllerBase //everything you need for a web API controller
    {
        private readonly BugsContext db;

        public TicketsV2Controller(BugsContext db)
        {
            this.db = db;
        }


        //[EnableQuery] //tickets?$filter=id gt 1-- can replace the filter code below (library still in preview)
        [HttpGet] 
        public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter) 
        {
            //dbSet implements IQueryable
            IQueryable<Ticket> tickets = db.Tickets;
            //can set the filters here
            if (ticketQueryFilter != null)
            {
                //if a filter exists
                if (ticketQueryFilter.Id.HasValue)
                    tickets = tickets.Where(x => x.TicketId == ticketQueryFilter.Id);
                //if contains in title to search/filter
                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Title))
                    tickets = tickets.Where(x => x.Title.Contains(ticketQueryFilter.Title,
                        StringComparison.OrdinalIgnoreCase));
                //if contains in description to search/filter
                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Description))
                    tickets = tickets.Where(x => x.Title.Contains(ticketQueryFilter.Description,
                        StringComparison.OrdinalIgnoreCase));
            }

            //ex. https://localhost:5001/api/tickets?title=1&api-version=2.0
            return Ok(await tickets.ToListAsync());
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
        [Ticket_EnsureDescriptionPresentActionFilterAttribute]
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


        [HttpPut("{id}")]
        [Ticket_EnsureDescriptionPresentActionFilter]
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
