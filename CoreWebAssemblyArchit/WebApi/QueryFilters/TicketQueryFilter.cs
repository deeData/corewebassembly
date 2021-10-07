using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.QueryFilters
{
    //filter the list of tickets- in v2
    public class TicketQueryFilter
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
