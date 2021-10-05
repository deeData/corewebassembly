using CoreWebApp.Models;
using System;
using System.Collections.Generic;
//can be used anywhere for data validation
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.ModelValidations
{
    public class Ticket_EnsureDueDateIsInFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = (Ticket)validationContext.ObjectInstance;

            //only applies when creating a new ticket
            if (ticket != null && ticket.TicketId == null)
            {
                if (ticket.DueDate.HasValue && ticket.DueDate.Value < DateTime.Now)
                {
                    return new ValidationResult("Due date has to be in the future.");
                }
            }
            return ValidationResult.Success;

        }

    }
}
