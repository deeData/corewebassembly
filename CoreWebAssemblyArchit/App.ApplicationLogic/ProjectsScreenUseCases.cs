using App.Repository.webApi;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationLogic
{
    class ProjectsScreenUseCases
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjectsAsync()
        {
            //add biz logic here
            return await projectRepository.GetAsync();
        }



         



    }
}
