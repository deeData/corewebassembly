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

Console.WriteLine($"================ delete project id {pId}");
await DeleteProject(pId);
await GetProjects();

Console.WriteLine($"==============TEST TICKETS===============");
await TestTickets();

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

async Task DeleteProject(int projectId)
{
    ProjectRepository repository = new(apiExecuter);
    await repository.DeleteAsync(projectId);
}








#region Test Tickets
async Task TestTickets()
{
    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading all tickets...");
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading contains 1...");
    await GetTickets("1");

    Console.WriteLine("////////////////////");
    Console.WriteLine("Create a ticket...");
    var tId = await CreateTicket();
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Update a ticket...");
    var ticket = await GetTicketById(tId);
    await UpdateTicket(ticket);
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Delete a ticket...");
    await DeleteTicket(tId);
    await GetTickets();
}



async Task GetTickets(string filter = null)
{
    TicketRepository ticketRepository = new(apiExecuter);
    var tickets = await ticketRepository.GetAsync(filter);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"Ticket: {ticket.Title}");
    }
}

async Task<Ticket> GetTicketById(int id)
{
    TicketRepository ticketRepository = new(apiExecuter);
    var ticket = await ticketRepository.GetByIdAsync(id);
    return ticket;
}

async Task<int> CreateTicket()
{
    TicketRepository ticketRepository = new(apiExecuter);
    return await ticketRepository.CreateAsync(new Ticket
    {
        ProjectId = 2,
        Title = "This a very difficult.",
        Description = "Something is wrong on the server."
    });
}

async Task UpdateTicket(Ticket ticket)
{
    TicketRepository ticketRepository = new(apiExecuter);
    ticket.Title += " Updated";
    await ticketRepository.UpdateAsync(ticket);
}

async Task DeleteTicket(int id)
{
    TicketRepository ticketRepository = new(apiExecuter);
    await ticketRepository.DeleteAsync(id);
}

#endregion










