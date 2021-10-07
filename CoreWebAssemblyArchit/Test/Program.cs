using App.Repository.webApi;
using App.Repository.webApi.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;



HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:44349", httpClient);

//TESTING webApi repository requests for data------- see ProjectRepository.cs
await GetProjects();

Console.WriteLine("================ reading project tix");
await GetProjectTickets(2);

int pId = await CreateProject();
Console.WriteLine($"CREATED PROJECT with ID: {pId}");
await GetProjects();

var project = await GetProject(pId);
await UpdateProject(project);
Console.WriteLine($"================ update project id {pId}");
await GetProjects();



async Task GetProjects()
{
    ProjectRepository repository = new(apiExecuter);
    var projects = await repository.GetAsync();
    foreach (var project in projects)
    {
        System.Console.WriteLine($"Project: {project.Name}");
    }
}

async Task<Project> GetProject(int id)
{
    ProjectRepository repository = new(apiExecuter);
    return await repository.GetByIdAsync(id);
}

async Task GetProjectTickets(int projectId)
{
    var project = await GetProject(projectId);
    Console.WriteLine($"Project: {project.Name}");

    ProjectRepository repository = new(apiExecuter);
    var tickets = await repository.GetProjectTicketsAsync(projectId);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($" Ticket: {ticket.Title}" );
    }
}

async Task<int> CreateProject()
{
    var project = new Project { Name = "Another Project" };
    ProjectRepository repository = new(apiExecuter);
    return await repository.CreateAsync(project);
}

async Task UpdateProject(Project project)
{
    ProjectRepository repository = new(apiExecuter);
    project.Name += " UPDATED";
    await repository.UpdateAsync(project);
}


