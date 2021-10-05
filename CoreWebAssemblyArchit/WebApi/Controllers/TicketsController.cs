using Core.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        //attribute routing
        //[Route("api/tickets")]
        //IActionResult returns all types- is generic
        public IActionResult Get() 
        {
            //400s user error, 500s server error
            return Ok("Reading all the tix");
        }

        [HttpGet("{id}")]
        //[Route("api/tickets/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading ticket #{id}.");
        }

        [HttpPost]
        //[Route("api/tickets")]
        public IActionResult PostV1([FromBody] Ticket ticket)
        {
            //Ok automatically serializes the obj into json
            return Ok(ticket);
        }


        [HttpPost]
        //**********should probably be /api/V2/tickets
        //v2 uses action filters so that change won't apply to v1
        [Route("V2/")]
        //add action filter attribute
        //[Ticket_EnsureEnteredDate]
        public IActionResult PostV2([FromBody] Ticket ticket)
        {
            //Ok automatically serializes the obj into json
            return Ok(ticket);
        }


        [HttpPut]
        //[Route("api/tickets")]
        public IActionResult Put()
        {
            return Ok("Updating a ticket.");
        }

        [HttpDelete("{id}")]
        //[Route("api/tickets/{id}")]
        public IActionResult Create(int id)
        {
            return Ok($"Deleting ticket #{id}.");
        }








    }
}
