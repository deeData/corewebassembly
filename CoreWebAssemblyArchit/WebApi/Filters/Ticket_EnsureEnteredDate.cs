using CoreWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.Filters
{
    public class Ticket_EnsureEnteredDate : ActionFilterAttribute
    {
        //using action filter you can apply to specific endpoint and not all instances of the model
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //base.OnActionExecuting(context);
            
            //apply to POST v2
            var ticket = context.ActionArguments["ticket"] as Ticket;
            if (ticket !=null && !string.IsNullOrWhiteSpace(ticket.Owner) 
                && ticket.EnteredDate.HasValue == false)
            {
                //enter the field name. ModelState is avail in MVC
                context.ModelState.AddModelError("EnteredDate", "Entered Date is Required.");
                //action filter will short circuit to return a bad request
                //context.Result = new BadRequestObjectResult(context.ModelState);

                //to make the error message format consistent, also has more error info
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemDetails);

            }
        }
    }
}
