using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.Filters
{
    //apply resournce filter at controller level
    public class Version1DiscontinueResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //not used because want to apply when coming in, not coming out
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2"))
            {
                //retire by short circuit- if returning a Result, you are short circuiting
                context.Result = new BadRequestObjectResult(
                    new
                    {
                        Versioning = new[] {"This version of the API has expired, please use the latest version." }
                    }

                    ); 
            }
        }








    }
}
