using CoreWebApp.ModelValidations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace CoreWebApp.Models
{
    public class Ticket
    {
        //[FromQuery(Name = "tid")] - usually do not get primitive type from obj/body
        public int? TicketId { get; set; }
        //[FromRoute(Name = "pid")] - - usually do not get primitive type from obj/body
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        //decorate the DueDate prop
        [Ticket_EnsureDueDateForTicketOwner]
        [Ticket_EnsureDueDateIsInFuture]
        public DateTime? DueDate { get; set; }
        public DateTime? EnteredDate { get; set; }
    }
}
