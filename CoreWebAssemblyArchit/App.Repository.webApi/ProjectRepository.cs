using App.Repository.webApi.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.webApi
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        //repository is expecting an obj that executes IWebApiExecuter
        //this repository is only dependent on the interface and not its base class
        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Project>> GetAsync()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects");

        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await webApiExecuter.InvokeGet<Project>($"api/projects/{id}");
        }

        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId)
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Ticket>>($"api/projects/{projectId}/tickets");
        }

        public async Task<int> CreateAsync(Project project)
        {
            project = await webApiExecuter.InvokePost("api/projects", project);
            return project.ProjectId;
        }

        public async Task UpdateAsync(Project project)
        {
            await webApiExecuter.InvokePut($"api/projects/{project.ProjectId}", project);
        }

        public async Task DeleteAsync(int projectId)
        {
            await webApiExecuter.InvokeDelete($"api/projects/{projectId}");
        }





    }
}
