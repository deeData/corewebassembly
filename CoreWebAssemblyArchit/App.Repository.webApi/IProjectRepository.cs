using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Repository.webApi
{
    public interface IProjectRepository
    {
        Task<int> CreateAsync(Project project);
        Task DeleteAsync(int projectId);
        Task<IEnumerable<Project>> GetAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId);
        Task UpdateAsync(Project project);
    }
}