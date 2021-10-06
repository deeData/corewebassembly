
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
    //Model validation happens automatically in the ApiController
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly BugsContext db;
        public ProjectsController(BugsContext db)
        {
            this.db = db;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //400s user error, 500s server error
            return Ok(await db.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        //get from route is the id
        public async Task<IActionResult> GetById(int id)
        {
            var project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                //404
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            //this helper function finds the created obj and returns the obj with 201
            return CreatedAtAction(nameof(GetById),
                new { id = project.ProjectId },
                project
                );
        }

        [HttpPut("{id}")]
        //updates are PUT
        public async Task<IActionResult> Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();
            //to set that it is modified
            db.Entry(project).State = EntityState.Modified;
            try
            {
               await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (db.Projects.Find(id) == null)
                    return NotFound();
                //will return a server error 500
                throw;
            }
            
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Create(int id)
        {
            //a soft delete is avaliable which is used in most production, but below is full delete
            var project = await db.Projects.FindAsync(id);
            if (project == null)
                return NotFound();
            db.Projects.Remove(project);
            await db.SaveChangesAsync();
            //return deleted object
            return Ok(project);
        }


        //uses query string data binding
        //api/projects/{pid}/tickets?tid={tid}
        [HttpGet]
        [Route("{pid}/tickets")]
        public async Task<IActionResult> GetProjectTicket(int pid)
        {
            //looking for FK so need to use Where and not Find.
            var tickets = await db.Tickets.Where(t => t.ProjectId == pid).ToListAsync();
            if (tickets == null || tickets.Count <= 0)
                return NotFound();
            
            return Ok(tickets);
        }


        ////below combines routing and query binding-- usually do not use an object to get a primitive type
        //[HttpGet]
        //[Route("{pid}/tickets")]
        ////look at properties of Ticket class for its model binding
        ////for complex types like a class object, it will look in the body- so need to specify FromQuery
        ////example - /api/projects/56/tickets?tid=3&title=abc
        //public IActionResult GetProjectTicket([FromQuery]Ticket ticket)
        //{
        //    if (ticket == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (ticket.TicketId == 0)
        //    {
        //        return Ok($"Reading all tix belonging to project #{ticket.ProjectId}.");
        //    }
        //    return Ok($"Reading project #{ticket.ProjectId} and ticket #{ticket.TicketId} and title is {ticket.Title}.");
        //}











    }
}
