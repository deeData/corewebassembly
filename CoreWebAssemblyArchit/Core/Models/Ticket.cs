using Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    //ticket for each bug fix
    public class Ticket
    {
        public int? TicketId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Owner { get; set; }
        [Ticket_EnsureReportDatePresent]
        public DateTime? ReportDate { get; set; }
        //c# doesn't need the ending "Attribute" to bind/reference
        [Ticket_EnsureDueDatePresentAttribute]
        [Ticket_EnsureFutureDueDateOnCreation]
        [Ticket_EnsureDueDateAfterReportDate]
        public DateTime? DueDate { get; set; }
        public Project Project { get; set; }


        ///place validations in plain c# classes so that if tech changes, they can still be reused.

        ///when creating a ticket, if due date is entered, it has to be in the future
        public bool ValidateFutureDueDate()
        {
            if (TicketId.HasValue) return true;
            if (!DueDate.HasValue) return true;

            return (DueDate.Value > DateTime.Now);
        }

        ///when owner is assigned, the report date has to be present
        public bool ValidateReportDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return ReportDate.HasValue;
        }

        ///when owner is assigned, the due date has to be present
        public bool ValidateDueDatePresence() 
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return DueDate.HasValue;
        }

        ///when due date and report date are present, due date has to be after report date
        public bool ValidateDueDateAfterReportDate() 
        {
            if (!DueDate.HasValue || !ReportDate.HasValue) return true;

            return DueDate.Value.Date > ReportDate.Value.Date;
        }


        ///USED in V2 of API endpoints
        public bool ValidateDescription() 
        {
            //return true if blank
            return string.IsNullOrWhiteSpace(Description);
        }



    }

}
