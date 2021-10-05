using CoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.ModelValidations
{
    public class Ticket_EnsureDueDateForTicketOwner : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);

            //get model
            var ticket = (Ticket)validationContext.ObjectInstance;
            
            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                //ticket has owner but no due date
                if (!ticket.DueDate.HasValue)
                {
                    return new ValidationResult("Due date is required when a ticket has an owner.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
