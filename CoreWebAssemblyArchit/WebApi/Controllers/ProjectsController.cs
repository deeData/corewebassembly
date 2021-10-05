
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //400s user error, 500s server error
            return Ok("Reading all the projects");
        }

        [HttpGet("{id}")]
        //get from route is the id
        public IActionResult GetById(int id)
        {
            return Ok($"Reading project #{id}.");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating a project");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project");
        }

        [HttpDelete("{id}")]
        public IActionResult Create(int id)
        {
            return Ok($"Deleting project #{id}.");
        }


        //uses query string data binding
        //api/projects/{pid}/tickets?tid={tid}
        [HttpGet]
        [Route("{pid}/tickets")]
        public IActionResult GetProjectTicket(int pid, [FromQuery] int tid)
        {
            //if no query string param, tid defaults to zero
            if (tid == 0)
            {
                return Ok($"Reading all tix belonging to project #{pid}.");
            }
            return Ok($"Reading project #{pid} and ticket #{tid}.");
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
