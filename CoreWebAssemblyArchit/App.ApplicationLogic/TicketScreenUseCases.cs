using App.Repository.webApi;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class TicketScreenUseCases : ITicketScreenUseCases
    {
        private readonly IProjectRepository projectRepository;

        //using project endpoints
        public TicketScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Ticket>> ViewTickets(int projectId)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId);
        }






    }
}
